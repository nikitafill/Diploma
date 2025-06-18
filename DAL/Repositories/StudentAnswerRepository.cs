using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace DiplomaProject.DAL.Repositories
{
    public class StudentAnswerRepository : GenericRepository<StudentAnswer>, IStudentAnswerRepository
    {
        public StudentAnswerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<StudentAnswer>> GetByExperimentIdAsync(int experimentId)
        {
            return await _dbSet.Where(a => a.ExperimentGroupId == experimentId).ToListAsync();
        }
    }
}
