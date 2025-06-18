using DiplomaProject.DAL.Models;
using static IndexModel;

namespace DiplomaProject.BLL.Services.Interfaces
{
    public interface IExperimentService
    {
        Task<FrameResponse> AnalyzeVideoAsync(IFormFile videoFile, int? groupId, double timeInSeconds);
        Task SaveRadiiAsync(int analysisId, float pixelsPerCm, float centreX, float centreY, List<float> radiiPixels);
        Task<VideoAnalysis> GetAnalysisAsync(int id);
        List<float> GetRadiiByVideoId(int videoId);
        Task<List<FrameResponse>> AnalyzeVideoMultipleFramesAsync(IFormFile videoFile, int? groupId, double startTime, double timeStep);
        Task<List<VideoAnalysis>> GetExperimentsByGroupAsync(int groupId);
    }

}
