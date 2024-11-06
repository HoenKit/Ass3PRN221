using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.SignalR;
using PRN221ExampleWeb.Hubs;

namespace PRN221ExampleWeb.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserRepository _userRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public EditModel(UserRepository userRepository, IHubContext<SignalRServer> hubContext)
        {
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var user =  _userRepository.GetUserById((int)id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
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

           _userRepository.UpdateUser(User);
            _hubContext.Clients.All.SendAsync("UserCreated", User.Id);

            return RedirectToPage("./Index");
        }

    }
}
