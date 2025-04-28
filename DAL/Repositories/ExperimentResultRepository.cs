using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.DAL.Repositories
{
    public class ExperimentResultRepository : GenericRepository<ExperimentResult>, IExperimentResultRepository
    {
        public ExperimentResultRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ExperimentResult?> GetByExperimentIdAsync(int experimentId)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.ExperimentId == experimentId);
        }
    }
}
