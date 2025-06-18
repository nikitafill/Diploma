using DiplomaProject.DAL.Models;

namespace DiplomaProject.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByLoginAsync(string login);
    }
}
