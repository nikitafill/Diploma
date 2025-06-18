using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using DiplomaProject.BLL;
using DiplomaProject.DAL.Models;
using DiplomaProject.BLL.Services.Interfaces;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

[Authorize(Roles = "�������,�������������")]
public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IExperimentService _service;
    private readonly IExperimentGroupService _serviceGroup;
    public IndexModel(IHttpClientFactory httpClientFactory, IExperimentService service, IExperimentGroupService serviceGroup)
    {
        _httpClientFactory = httpClientFactory;
        _service = service;
        _serviceGroup = serviceGroup;
    }

    [BindProperty]
    public IFormFile VideoFile { get; set; }

    [BindProperty]
    public double TimeInSeconds { get; set; } 
    [BindProperty]
    public double StartTime { get; set; } 

    [BindProperty]
    public double? TimeStep { get; set; } 

    public List<FrameResponse> ExtractedFrames { get; set; } = new(); 
    [BindProperty(SupportsGet = true)]
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public List<FrameResponse> GroupFrames { get; set; } = new();
    public async Task<IActionResult> OnGetAsync()
    {
        if (GroupId > 0)
        {
            var experimentGroup = await _serviceGroup.GetByIdAsync(GroupId);
            GroupName = experimentGroup.Name;

            var groupAnalyses = await _service.GetExperimentsByGroupAsync(GroupId);
            
            GroupFrames = groupAnalyses.Select(a => new FrameResponse
            {
                Id = a.Id,
                FrameUrl = a.ExtractedFramePath
            }).ToList();
        }

        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var experimentGroup = await _serviceGroup.GetByIdAsync(GroupId);
        GroupName = experimentGroup.Name;
        if (VideoFile == null || VideoFile.Length == 0)
        {
            ModelState.AddModelError("", "�������� ���������.");
            return Page();
        }

        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://localhost:7143");

        using var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(VideoFile.OpenReadStream());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(VideoFile.ContentType);
        content.Add(streamContent, "videoFile", VideoFile.FileName);

        HttpResponseMessage response;
        Console.WriteLine(StartTime);
        Console.WriteLine(TimeStep);
        if (TimeStep.HasValue && TimeStep > 0)
        {
            
            string url = $"api/video/upload-multiple?startTime={StartTime}&timeStep={TimeStep.Value}&groupId={GroupId}";
            response = await client.PostAsync(url, content);
        }
        else
        {
            
            string url = $"api/video/upload-single?timeInSeconds={StartTime}&groupId={GroupId}";
            response = await client.PostAsync(url, content);
        }

        if (response.IsSuccessStatusCode)
        {
            if (TimeStep.HasValue && TimeStep > 0)
                ExtractedFrames = await response.Content.ReadFromJsonAsync<List<FrameResponse>>();
            else
            {
                var single = await response.Content.ReadFromJsonAsync<FrameResponse>();
                ExtractedFrames = new List<FrameResponse> { single };
            }

            return Page();
        }

        ModelState.AddModelError("", "������ ��� ���������� �����(��).");
        return Page();
    }
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToPage("/Auth");
    }
}
