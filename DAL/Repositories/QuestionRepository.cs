using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;

namespace DiplomaProject.DAL.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context) { }

    }
}
