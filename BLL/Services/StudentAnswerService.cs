using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;

namespace DiplomaProject.BLL.Services
{
    public class StudentAnswerService : IStudentAnswerService
    {
        private readonly IStudentAnswerRepository _repository;

        public StudentAnswerService(IStudentAnswerRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<StudentAnswer>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<StudentAnswer?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(StudentAnswer answer)
        {
            await _repository.AddAsync(answer);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudentAnswer answer)
        {
            _repository.Update(answer);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer != null)
            {
                _repository.Delete(answer);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
