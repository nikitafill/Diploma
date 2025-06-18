using DiplomaProject.DAL.Models;
using System;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace DiplomaProject.DAL.Repositories
{
    public class ExperimentRepository : IExperimentRepository
    {
        private readonly ApplicationDbContext _context;

        public ExperimentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VideoAnalysis> GetAsync(int id)
        {
            return await _context.VideoAnalyses.Include(v => v.Radii).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<VideoAnalysis>> GetAllAsync()
        {
            return await _context.VideoAnalyses.Include(v => v.Radii).ToListAsync();
        }

        public async Task AddAsync(VideoAnalysis analysis)
        {
            await _context.VideoAnalyses.AddAsync(analysis);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddGroupAsync(ExperimentGroup group)
        {
            await _context.ExperimentGroups.AddAsync(group);
        }
        public async Task<List<VideoAnalysis>> GetExperimentsByGroupAsync(int groupId)
        {
            return await _context.VideoAnalyses
                .Where(a => a.GroupId == groupId)
                .Include(a => a.Radii)
                .ToListAsync();
        }
    }
}
