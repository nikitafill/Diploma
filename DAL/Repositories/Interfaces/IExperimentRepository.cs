using DiplomaProject.DAL.Models;

namespace DiplomaProject.DAL.Repositories.Interfaces
{
    public interface IExperimentRepository
    {
        Task<VideoAnalysis> GetAsync(int id);
        Task<List<VideoAnalysis>> GetAllAsync();
        Task AddAsync(VideoAnalysis analysis);
        Task SaveChangesAsync();
        Task AddGroupAsync(ExperimentGroup group);
        Task<List<VideoAnalysis>> GetExperimentsByGroupAsync(int groupId);
    }

}
