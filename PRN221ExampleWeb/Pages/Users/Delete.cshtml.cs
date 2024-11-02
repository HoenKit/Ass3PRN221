using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.SignalR;
using PRN221ExampleWeb.Hubs;

namespace PRN221ExampleWeb.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserRepository _userRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public DeleteModel(UserRepository userRepository, IHubContext<SignalRServer> hubContext)
        {
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.GetUserById((int) id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.GetUserById((int) id);
            if (user != null)
            {
                User = user;
                _userRepository.DeleteUser(user.Id);
                _hubContext.Clients.All.SendAsync("UserCreated", User.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
