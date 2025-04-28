using DiplomaProject.DAL.Models;

namespace DiplomaProject.DAL.Repositories.Interfaces
{
    public interface IExperimentResultRepository : IGenericRepository<ExperimentResult>
    {
        Task<ExperimentResult?> GetByExperimentIdAsync(int experimentId);
    }
}
