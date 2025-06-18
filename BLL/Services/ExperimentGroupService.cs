using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;

namespace DiplomaProject.BLL.Services
{
    public class ExperimentGroupService : IExperimentGroupService
    {
        private readonly IExperimentGroupRepository _repository;

        public ExperimentGroupService(IExperimentGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExperimentGroup>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<ExperimentGroup?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(ExperimentGroup group)
        {
            await _repository.AddAsync(group);
            await _repository.SaveChangesAsync();
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
