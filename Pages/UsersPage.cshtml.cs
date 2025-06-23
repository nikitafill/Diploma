using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiplomaProject.Pages
{
    [Authorize(Roles = "Преподаватель")]
    public class UsersPageModel : PageModel
    {
        private readonly IUserService _userService;
        public UsersPageModel(IUserService userService)
        {
            _userService = userService;
        }
        public List<string> Roles { get; set; } = new() { "Студент", "Преподаватель" };
        public List<User> Users { get; set; } = new();
        [BindProperty]
        public User NewUser { get; set; } = new();
        public async Task OnGetAsync()
        {
            Users = (await _userService.GetAllAsync()).ToList();
        }
        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _userService.AddAsync(NewUser);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Auth");
        }
        [BindProperty]
        public User EditedUser { get; set; } = new();

        public async Task<IActionResult> OnPostEditAsync()
        {
            await _userService.UpdateAsync(EditedUser);
            return RedirectToPage();
        }
    }
}
