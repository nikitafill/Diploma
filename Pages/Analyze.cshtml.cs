using DiplomaProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using OpenCvSharp;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

[Authorize(Roles = "Студент,Преподаватель")]
public class AnalyzeModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IExperimentService _service;
    private readonly IWebHostEnvironment _env;

    public AnalyzeModel(IHttpClientFactory httpClientFactory, IExperimentService service, IWebHostEnvironment env)
    {
        _httpClientFactory = httpClientFactory;
        _service = service;
        _env = env;
    }
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    [BindProperty(SupportsGet = true)]
    public int GroupId { get; set; }
    public List<int> GroupExperimentsIds { get; set; } = new();
    public int? PrevId { get; set; }
    public int? NextId { get; set; }

    [BindProperty]
    public float PixelsPerCm { get; set; }
    [BindProperty]
    public float? CentreX { get; set; }

    [BindProperty]
    public float? CentreY { get; set; }

    [BindProperty]
    public string ExtractedFrameUrl { get; set; }
    public List<float> RadiiFromDb { get; set; } = new();
    [BindProperty]
    public float? WavelengthNm { get; set; }

    [BindProperty]
    public float? LensRadius { get; set; }
    [BindProperty]
    public float? I0 { get; set; }

    public byte[]? IntensityMapPng { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        // 1. Загрузить сам анализ
        var analysis = await _service.GetAnalysisAsync(Id);
        if (analysis == null)
            return NotFound();

        ExtractedFrameUrl = analysis.ExtractedFramePath;
        RadiiFromDb = analysis.Radii.Select(r => r.RadiusCm).OrderBy(r => r).ToList();
        GroupId = analysis.GroupId;
        // 2. Загрузить все анализы группы
        GroupExperimentsIds = (await _service.GetExperimentsByGroupAsync(analysis.GroupId))
                            .OrderBy(a => a.Id)
                            .Select(a => a.Id)
                            .ToList();

        WavelengthNm ??= 550.0f;
        I0 ??= 1.0f;

        LoadNavigationDataAsync();
        Extract1DIntensity();
        return Page();
    }
    public async Task<IActionResult> OnPostAsync([FromForm] List<float> radiiPixels)
    {
        if (PixelsPerCm <= 0 || radiiPixels.Count == 0)
        {
            ModelState.AddModelError("", "Недостаточно данных для анализа");
            return Page();
        }

        var client = _httpClientFactory.CreateClient();

        var form = new MultipartFormDataContent
        {
            { new StringContent(Id.ToString()), "id" },
            { new StringContent(PixelsPerCm.ToString()), "pixelsPerCm" },
            { new StringContent(CentreX.ToString()), "centreX" },
            { new StringContent(CentreY.ToString()), "centreY" },
        };

        for (int i = 0; i < radiiPixels.Count; i++)
        {
            form.Add(new StringContent(radiiPixels[i].ToString()), "radiiPixels");
        }

        var response = await client.PostAsync("https://localhost:7143/api/video/submit-radii", form);
        if (response.IsSuccessStatusCode)
        {
            await OnGetRadiiTableAsync();
            LoadNavigationDataAsync();
            return Page();
        }

        ModelState.AddModelError("", "Ошибка при отправке радиусов через API");
        LoadNavigationDataAsync();
        Extract1DIntensity();
        return Page();
    }
    public async Task<IActionResult> OnGetRadiiTableAsync()
    {
        var analysis = await _service.GetAnalysisAsync(Id);
        if (analysis == null)
        {
            ModelState.AddModelError("", "Анализ не найден");
            return Page();
        }

        ExtractedFrameUrl = analysis.ExtractedFramePath;
        RadiiFromDb = analysis.Radii
            .Select(r => r.RadiusCm)
            .OrderBy(r => r)
            .ToList();

        GroupExperimentsIds = (await _service.GetExperimentsByGroupAsync(analysis.GroupId))
        .OrderBy(a => a.Id)
        .Select(a => a.Id)
        .ToList();

        GroupId = analysis.GroupId;

        LoadNavigationDataAsync();
        Extract1DIntensity();
        return Page();
    }
    public List<double>? Intensities { get; set; }
    public double[] RadiiJson { get; set; } = new double[100];
    public double[] IntensitiesJson { get; set; } = new double[100];
    public async Task<IActionResult> OnPostCalculateLensRadiusAsync()
    {
        if (!WavelengthNm.HasValue || WavelengthNm <= 0)
        {
            ModelState.AddModelError("", "Укажите длину волны.");
            await OnGetRadiiTableAsync();
            LoadNavigationDataAsync();
            return Page();
        }

        var analysis = await _service.GetAnalysisAsync(Id);
        if (analysis == null || analysis.Radii == null || analysis.Radii.Count < 2)
        {
            ModelState.AddModelError("", "Недостаточно данных для расчета.");
            await OnGetRadiiTableAsync();
            LoadNavigationDataAsync();
            return Page();
        }

        RadiiFromDb = analysis.Radii
            .Select(r => r.RadiusCm)
            .OrderBy(r => r)
            .ToList();

        GroupId = analysis.GroupId;
        ExtractedFrameUrl = analysis.ExtractedFramePath;

        // Новый метод: разница радиусов делённая на число колец
        double rMin = RadiiFromDb.First();
        double rMax = RadiiFromDb.Last();
        int ringCount = RadiiFromDb.Count - 1; // число интервалов между кольцами

        if (ringCount == 0)
        {
            ModelState.AddModelError("", "Невозможно вычислить радиус: недостаточно колец.");
            await OnGetRadiiTableAsync();
            LoadNavigationDataAsync();
            return Page();
        }

        double wavelengthCm = WavelengthNm.Value /1000000; // перевод из нм в мм
        double tanAlpha = (rMax*rMax - rMin* rMin) / ringCount;
        LensRadius = (float)(tanAlpha / wavelengthCm);

        RadiiJson[0] = 0;
        double delta = LensRadius.Value - Math.Sqrt((LensRadius.Value * LensRadius.Value)); 
        IntensitiesJson[0] = Math.Pow(Math.Cos(2 * Math.PI / wavelengthCm * delta+ (Math.PI / 2)), 2);
        for (int i = 1; i < RadiiJson.Length; i++)
        {
            RadiiJson[i] = RadiiJson[i-1]+ 0.0068;
            delta = LensRadius.Value - Math.Sqrt((LensRadius.Value * LensRadius.Value) - (RadiiJson[i] * RadiiJson[i]));
            IntensitiesJson[i] = Math.Pow(Math.Cos(2 * Math.PI / wavelengthCm * delta + (Math.PI / 2)), 2);
        }

        Extract1DIntensity();
        
        GroupExperimentsIds = (await _service.GetExperimentsByGroupAsync(analysis.GroupId))
        .OrderBy(a => a.Id)
        .Select(a => a.Id)
        .ToList();

        GroupId = analysis.GroupId;

        LoadNavigationDataAsync();
        return Page();
    }
    public double[]? IntensityProfile { get; set; }
    public string? IntensityProfileJson => IntensityProfile != null
    ? JsonSerializer.Serialize(IntensityProfile)
    : null;
    public void Extract1DIntensity()
    {
        if (string.IsNullOrEmpty(ExtractedFrameUrl))
            return;

        var imagePath = Path.Combine(_env.WebRootPath, ExtractedFrameUrl.TrimStart('/'));

        if (!System.IO.File.Exists(imagePath))
            return;

        var analysis = _service.GetAnalysisAsync(Id).Result;
        if (analysis == null)
            return;
        IntensityProfile = ExtractHorizontalIntensityVector(imagePath, analysis.CentreY);
    }
    public double[] ExtractHorizontalIntensityVector(string imagePath, float centreY)
    {
        using var img = Cv2.ImRead(imagePath, ImreadModes.Grayscale);

        int y = (int)Math.Round(centreY);
        Console.WriteLine(y);
        if (y < 0 || y >= img.Rows)
            throw new ArgumentOutOfRangeException(nameof(centreY), "Координата Y выходит за границы изображения");

        using var rowMat = img.Row(y);
        var rowData = new byte[rowMat.Cols];
        Marshal.Copy(rowMat.Data, rowData, 0, rowMat.Cols);
        return rowData.Select(b => (double)b).ToArray();
    }
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToPage("/Auth");
    }
    public void LoadNavigationDataAsync()
    {
        int index = GroupExperimentsIds.IndexOf(Id);
        if (index > 0)
            PrevId = GroupExperimentsIds[index - 1];
        if (index >= 0 && index < GroupExperimentsIds.Count - 1)
            NextId = GroupExperimentsIds[index + 1];
    }
}