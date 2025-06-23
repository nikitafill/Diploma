using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.BLL.Services
{
    public class ExperimentGroupService : IExperimentGroupService
    {
        private readonly IExperimentGroupRepository _repository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ApplicationDbContext _context;

        public ExperimentGroupService(IExperimentGroupRepository repository,
                                      IQuestionRepository questionRepository,
                                      ApplicationDbContext context)
        {
            _repository = repository;
            _questionRepository = questionRepository;
            _context = context;
        }

        public async Task<IEnumerable<ExperimentGroup>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<ExperimentGroup?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(ExperimentGroup group)
        {
            await _repository.AddAsync(group);
            await _repository.SaveChangesAsync();

            var questions = await _questionRepository.GetAllAsync();
            var randomQuestions = questions.OrderBy(q => Guid.NewGuid()).Take(3).ToList();

            foreach (var question in randomQuestions)
            {
                _context.ExperimentGroupQuestions.Add(new ExperimentGroupQuestion
                {
                    ExperimentGroupId = group.Id,
                    QuestionId = question.Id
                });
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var group = await _repository.GetByIdAsync(id);
            if (group != null)
            {
                _repository.Delete(group);
                await _repository.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<ExperimentGroup>> GetByUserIdAsync(int userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }
    }

}
