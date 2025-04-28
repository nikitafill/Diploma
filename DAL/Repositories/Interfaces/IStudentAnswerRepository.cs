using DiplomaProject.DAL.Models;

namespace DiplomaProject.DAL.Repositories.Interfaces
{
    public interface IStudentAnswerRepository : IGenericRepository<StudentAnswer>
    {
        Task<IEnumerable<StudentAnswer>> GetByExperimentIdAsync(int experimentId);
    }

}
