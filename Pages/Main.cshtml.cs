using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiplomaProject.Pages
{
    [Authorize(Roles = "Студент,Преподаватель")]
    public class MainModel : PageModel
    {
        private readonly IExperimentGroupService _service;

        public MainModel(IExperimentGroupService service)
        {
            _service = service;
        }

        public List<ExperimentGroup> Groups { get; set; } = new();

        [BindProperty]
        public ExperimentGroup NewGroup { get; set; } = new();
        
        public int UserId { get; set; }

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                UserId = userId;
                Groups = (await _service.GetByUserIdAsync(UserId)).ToList();
            }
            else
            {
                
                RedirectToPage("/Auth");
            }
        }
        public async Task<IActionResult> OnPostAddAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                NewGroup.UserId = userId;
                await _service.AddAsync(NewGroup);
                return RedirectToPage(); 
            }
            return RedirectToPage("/Auth");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Auth");
        }
    }
}
