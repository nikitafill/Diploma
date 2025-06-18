using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using DiplomaProject.BLL.DTOs;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] DiplomaProject.BLL.DTOs.LoginRequest request)
    {
        var user = await _userService.AuthenticateAsync(request.Login, request.Password);
        if (user == null)
            return Unauthorized("Неверный логин или пароль");

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Login),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("UserId", user.Id.ToString())
    };

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal);

        return Ok(new { user.Id, user.Login, user.Role });
    }

}
