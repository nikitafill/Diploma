using DiplomaProject.DAL.Models;

namespace DiplomaProject.BLL.Services.Interfaces
{
    public interface IStudentAnswerService
    {
        Task<IEnumerable<StudentAnswer>> GetAllAsync();
        Task<StudentAnswer?> GetByIdAsync(int id);
        Task AddAsync(StudentAnswer question);
        Task UpdateAsync(StudentAnswer question);
        Task DeleteAsync(int id);
        Task<List<StudentAnswer>> GetAnswersByGroupAsync(int groupId);
    }
}
