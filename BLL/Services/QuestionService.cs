using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using DiplomaProject.DAL.Repositories.Interfaces;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _repository;

    public QuestionService(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Question>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Question?> GetByIdAsync(int id)
        => await _repository.GetByIdAsync(id);

    public async Task AddAsync(Question question)
    {
        await _repository.AddAsync(question);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Question question)
    {
        _repository.Update(question);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var question = await _repository.GetByIdAsync(id);
        if (question != null)
        {
            _repository.Delete(question);
            await _repository.SaveChangesAsync();
        }
    }
}
