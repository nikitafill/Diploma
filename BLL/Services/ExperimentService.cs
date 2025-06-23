using DiplomaProject.DAL.Models;
using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Repositories.Interfaces;
using OpenCvSharp;

namespace DiplomaProject.BLL.Services
{
    public class ExperimentService : IExperimentService
    {
        private readonly IExperimentRepository _repo;
        private readonly IExperimentGroupRepository _groupRepo;
        private readonly IWebHostEnvironment _env;

        public ExperimentService(IExperimentRepository repo, IExperimentGroupRepository groupRepo, IWebHostEnvironment env)
        {
            _repo = repo;
            _groupRepo = groupRepo;
            _env = env;
        }
        public async Task<FrameResponse> AnalyzeVideoAsync(IFormFile videoFile, int? groupId, double timeInSeconds = 0)
        {
            Console.WriteLine(timeInSeconds);
            if (!groupId.HasValue || groupId.Value == 0)
                throw new Exception("Не указана группа экспериментов.");

            var group = await _groupRepo.GetByIdAsync(groupId.Value);
            if (group == null)
                throw new Exception("Группа экспериментов не найдена.");

            var sourceFileName = Path.GetRandomFileName() + Path.GetExtension(videoFile.FileName);
            var videoPath = Path.Combine(_env.WebRootPath, "videos", sourceFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(videoPath)!);
            using (var stream = new FileStream(videoPath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            group.SourceName = "/videos/" + sourceFileName;
            group.FramesDirectory = $"/frames/group_{Guid.NewGuid()}";

            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, group.FramesDirectory.TrimStart('/')));
            await _repo.SaveChangesAsync();

            var frameFile = "frame_" + Guid.NewGuid() + ".jpg";
            var framePath = Path.Combine(_env.WebRootPath, group.FramesDirectory.TrimStart('/'), frameFile);

            Utilities.ExtractFrame(videoPath, framePath, timeInSeconds);

            var analysis = new VideoAnalysis
            {
                GroupId = group.Id,
                ExtractedFramePath = $"{group.FramesDirectory}/{frameFile}"
            };

            await _repo.AddAsync(analysis);
            await _repo.SaveChangesAsync();

            return new FrameResponse
            {
                FrameUrl = analysis.ExtractedFramePath,
                Id = analysis.Id
            };
        }

        public async Task<List<FrameResponse>> AnalyzeVideoMultipleFramesAsync(IFormFile videoFile, int? groupId, double startTime, double timeStep)
        {
            Console.WriteLine(startTime);
            Console.WriteLine(timeStep);
            if (!groupId.HasValue || groupId.Value == 0)
                throw new Exception("Не указана группа экспериментов.");

            var group = await _groupRepo.GetByIdAsync(groupId.Value);
            if (group == null)
                throw new Exception("Группа экспериментов не найдена.");

            var sourceFileName = Path.GetRandomFileName() + Path.GetExtension(videoFile.FileName);
            var sourceVideoPath = Path.Combine(_env.WebRootPath, "videos", sourceFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(sourceVideoPath)!);

            using (var stream = new FileStream(sourceVideoPath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            group.SourceName = "/videos/" + sourceFileName;
            group.FramesDirectory = $"/frames/group_{Guid.NewGuid()}";

            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, group.FramesDirectory.TrimStart('/')));
            //await _repo.SaveChangesAsync();

            var results = new List<FrameResponse>();
            var capture = new OpenCvSharp.VideoCapture(sourceVideoPath);
            double fps = capture.Fps;
            double totalFrames = capture.FrameCount;
            double durationSeconds = totalFrames / fps;

            for (double t = startTime; t <= durationSeconds; t += timeStep)
            {
                var frameFile = $"frame_{Guid.NewGuid()}.jpg";
                var framePath = Path.Combine(_env.WebRootPath, group.FramesDirectory.TrimStart('/'), frameFile);

                Utilities.ExtractFrame(sourceVideoPath, framePath, t);

                var analysis = new VideoAnalysis
                {
                    GroupId = group.Id,
                    ExtractedFramePath = $"{group.FramesDirectory}/{frameFile}"
                };

                await _repo.AddAsync(analysis);
                await _repo.SaveChangesAsync();

                results.Add(new FrameResponse
                {
                    FrameUrl = analysis.ExtractedFramePath,
                    Id = analysis.Id
                });
            }

            return results;
        }

        public async Task SaveRadiiAsync(int analysisId, float pixelsPerCm, float centreX, float centerY, List<float> radiiPixels)
        {
            Console.WriteLine($"Saving to DB: Id={analysisId}, px/cm={pixelsPerCm}, x and y coords = ({centreX},{centerY}) radii={radiiPixels.Count}");
            var analysis = await _repo.GetAsync(analysisId);
            if (analysis == null) throw new Exception("Анализ не найден");
            
            analysis.PixelsPerCm = pixelsPerCm; //масштаб
            analysis.CentreX = centreX;
            analysis.CentreY = centerY;
            analysis.Radii.Clear();
            float onPx = 0.18f/ pixelsPerCm;
            
            foreach (var px in radiiPixels)
            {
                var cm = onPx * px;
                analysis.Radii.Add(new RingRadius { RadiusCm = cm });
            }

            await _repo.SaveChangesAsync();
        }
        public async Task<VideoAnalysis> GetAnalysisAsync(int id)
        {
            var analysis = await _repo.GetAsync(id);
            if (analysis == null) throw new Exception("Анализ не найден");
            return analysis;
        }
        public List<float> GetRadiiByVideoId(int videoId)
        {
            var analysis = _repo.GetAsync(videoId).Result;
            if (analysis == null || analysis.Radii == null)
                return new List<float>();

            return analysis.Radii
                .Select(r => r.RadiusCm * analysis.PixelsPerCm)
                .OrderBy(r => r)
                .ToList();
        }
        public async Task<List<VideoAnalysis>> GetExperimentsByGroupAsync(int groupId)
        {
            return await _repo.GetExperimentsByGroupAsync(groupId);
        }
    }
}