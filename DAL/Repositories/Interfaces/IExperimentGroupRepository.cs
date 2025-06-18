using DiplomaProject.DAL.Models;

namespace DiplomaProject.DAL.Repositories.Interfaces
{
    public interface IExperimentGroupRepository : IGenericRepository<ExperimentGroup>
    {
        Task<IEnumerable<ExperimentGroup>> GetByUserIdAsync(int userId);
    }
}
