using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.DAL.Repositories
{
    public class ExperimentRepository : GenericRepository<Experiment>, IExperimentRepository
    {
        public ExperimentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Experiment>> GetByUserIdAsync(int userId)
        {
            return await _dbSet.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
