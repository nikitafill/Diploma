using DiplomaProject.BLL.Services;
using DiplomaProject.BLL.Services.Interfaces;
using DiplomaProject.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiplomaProject.Pages
{
    [Authorize(Roles = "Преподаватель")]
    public class QuestionPageModel : PageModel
    {
        private readonly IQuestionService _service;

        public QuestionPageModel(IQuestionService service)
        {
            _service = service;
        }

        public List<Question> Questions { get; set; } = new();

        [BindProperty]
        public Question NewQuestion { get; set; } = new();

        public async Task OnGetAsync()
        {
            Questions = (await _service.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _service.AddAsync(NewQuestion);
            return RedirectToPage();
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
        [BindProperty]
        public Question EditedQuestion { get; set; } = new();

        public async Task<IActionResult> OnPostEditAsync()
        {
            await _service.UpdateAsync(EditedQuestion);
            return RedirectToPage();
        }

    }

}
