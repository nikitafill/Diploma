using DiplomaProject.DAL.Models;

namespace DiplomaProject.BLL.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllAsync();
        Task<Question?> GetByIdAsync(int id);
        Task AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
    }
}