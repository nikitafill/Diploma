using DiplomaProject.DAL.Models;

namespace DiplomaProject.DAL.Repositories.Interfaces
{
    public interface IExperimentRepository : IGenericRepository<Experiment>
    {
        Task<IEnumerable<Experiment>> GetByUserIdAsync(int userId);
    }
}
