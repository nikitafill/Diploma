using DiplomaProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using System.Text.Json;

[Route("api/video")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IExperimentService _service;

    public VideoController(IExperimentService service)
    {
        _service = service;
    }
    [HttpPost("upload-single")]
    public async Task<IActionResult> UploadSingle([FromForm] IFormFile videoFile, [FromQuery] int? groupId = null, [FromQuery] double timeInSeconds = 0)
    {
        if (videoFile == null || videoFile.Length == 0)
            return BadRequest("Файл не выбран");
        try
        {
            Console.WriteLine(timeInSeconds);
            var result = await _service.AnalyzeVideoAsync(videoFile, groupId, timeInSeconds);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("upload-multiple")]
    public async Task<IActionResult> UploadMultiple([FromForm] IFormFile videoFile, [FromQuery] int? groupId = null, [FromQuery] double startTime = 0, [FromQuery] double timeStep = 1)
    {
        if (videoFile == null || videoFile.Length == 0)
            return BadRequest("Файл не выбран");
        Console.WriteLine(startTime);
        Console.WriteLine(timeStep);
        var results = await _service.AnalyzeVideoMultipleFramesAsync(videoFile, groupId, startTime, timeStep);
        return Ok(results); 
    }

    [HttpPost("submit-radii")]
    public async Task<IActionResult> SubmitRadii([FromForm] int id, [FromForm] float pixelsPerCm, [FromForm] float centreX, [FromForm] float centreY, [FromForm] List<float> radiiPixels )
    {
        await _service.SaveRadiiAsync(id, pixelsPerCm, centreX, centreY, radiiPixels);
        return Ok(new { message = "Радиусы сохранены", status = "success" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var analysis = await _service.GetAnalysisAsync(id);
        return Ok(analysis);
    }
    [HttpGet("video/{id}/radii")]
    public IActionResult GetRadii(int id)
    {
        var radii = _service.GetRadiiByVideoId(id); 
        return Ok(radii);
    }

}