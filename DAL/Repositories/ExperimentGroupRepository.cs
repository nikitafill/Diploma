using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.DAL.Repositories
{
    public class ExperimentGroupRepository : GenericRepository<ExperimentGroup>, IExperimentGroupRepository
    {
        public ExperimentGroupRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<ExperimentGroup>> GetByUserIdAsync(int userId)
        {
            return await _dbSet.Where(g => g.UserId == userId).ToListAsync();
        }
    }
}
