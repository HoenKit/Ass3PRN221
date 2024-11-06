using BusinessObject.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221ExampleWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserRepository _userRepository;

        public IndexModel(ILogger<IndexModel> logger, UserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_userRepository.Login(User.UserName, User.Password))
            {
                HttpContext.Session.SetString("UserName", User.UserName);
                return RedirectToPage("./Categories/Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid login.");
            
            return Page();
        }
        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
