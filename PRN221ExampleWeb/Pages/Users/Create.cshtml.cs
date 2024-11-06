using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.SignalR;
using PRN221ExampleWeb.Hubs;

namespace PRN221ExampleWeb.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserRepository _userRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(UserRepository userRepository, IHubContext<SignalRServer> hubContext)
        {
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userRepository.CreateUser(User);
            _hubContext.Clients.All.SendAsync("UserCreated", User.Id);

            return RedirectToPage("./Index");
        }
    }
}
