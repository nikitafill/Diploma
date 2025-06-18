using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Login,
                u.Role,
                u.GroupName
            }));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] User user)
        {
            await _service.AddAsync(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id) return BadRequest();
            await _service.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}



