using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.DAL.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context) { }
        public async Task<List<Question>> GetQuestionsByGroupAsync(int groupId)
        {
            return await  _context.ExperimentGroupQuestions
                .Where(eq => eq.ExperimentGroupId == groupId)
                .Select(eq => eq.Question)
                .ToListAsync();
        }
    }
}
