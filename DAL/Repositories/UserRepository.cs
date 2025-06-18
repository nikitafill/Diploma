using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
