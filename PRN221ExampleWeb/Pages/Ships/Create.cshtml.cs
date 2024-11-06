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

namespace PRN221ExampleWeb.Pages.Ships
{
    public class CreateModel : PageModel
    {
        private readonly ShipRepository _shipRepository;
        private readonly UserRepository _userRepository;
        private readonly BookRepository _bookRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(ShipRepository shipRepository, UserRepository userRepository, BookRepository bookRepository, IHubContext<SignalRServer> hubContext)
        {
            _shipRepository = shipRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index");
            }
            ViewData["BookId"] = new SelectList(_bookRepository.GetBooksList(), "Id", "BookName");
            ViewData["UserOrderId"] = new SelectList(_userRepository.GetUsersList(), "Id", "UserName");
            ViewData["UserShipId"] = new SelectList(_userRepository.GetUsersList(), "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Ship Ship { get; set; } = default!;

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

            _shipRepository.CreateShip(Ship);
            _hubContext.Clients.All.SendAsync("ShipCreated", Ship.Id);

            return RedirectToPage("./Index");
        }
    }
}
