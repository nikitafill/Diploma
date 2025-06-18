using DiplomaProject.DAL.Models;

namespace DiplomaProject.BLL.Services.Interfaces
{
    public interface IExperimentGroupService
    {
        Task<IEnumerable<ExperimentGroup>> GetAllAsync();
        Task<ExperimentGroup?> GetByIdAsync(int id);
        Task AddAsync(ExperimentGroup group);
        Task DeleteAsync(int id);
        Task<IEnumerable<ExperimentGroup>> GetByUserIdAsync(int userId);
    }
}
