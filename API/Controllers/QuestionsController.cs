using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _service;

    public QuestionsController(IQuestionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Question>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> Get(int id)
    {
        var question = await _service.GetByIdAsync(id);
        if (question == null) return NotFound();
        return Ok(question);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Question question)
    {
        await _service.AddAsync(question);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Question question)
    {
        if (id != question.Id) return BadRequest();
        await _service.UpdateAsync(question);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}