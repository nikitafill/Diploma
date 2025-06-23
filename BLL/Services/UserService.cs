using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;

namespace DiplomaProject.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(User user)
        {
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _repository.Update(user);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user != null)
            {
                _repository.Delete(user);
                await _repository.SaveChangesAsync();
            }
        }
        public async Task<User?> AuthenticateAsync(string login, string password)
        {
            var users = await _repository.FindAsync(u => u.Login == login && u.PasswordHash == password);
            return users.FirstOrDefault();
        }
    }

}
