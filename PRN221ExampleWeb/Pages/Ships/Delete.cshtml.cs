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

namespace PRN221ExampleWeb.Pages.Ships
{
    public class DeleteModel : PageModel
    {
        private readonly ShipRepository _shipRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public DeleteModel(ShipRepository shipRepository, IHubContext<SignalRServer> hubContext)
        {
            _shipRepository = shipRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Ship Ship { get; set; } = default!;

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

            var ship = _shipRepository.GetShipById((int)id);

            if (ship == null)
            {
                return NotFound();
            }
            else
            {
                Ship = ship;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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

            var ship = _shipRepository.GetShipById((int)id);
            if (ship != null)
            {
                Ship = ship;
                _shipRepository.DeleteShip(ship.Id);
                _hubContext.Clients.All.SendAsync("ShipCreated", Ship.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
