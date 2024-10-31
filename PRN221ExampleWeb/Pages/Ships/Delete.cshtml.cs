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

namespace PRN221ExampleWeb.Pages.Ships
{
    public class DeleteModel : PageModel
    {
        private readonly ShipRepository _shipRepository;

        public DeleteModel(ShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        [BindProperty]
        public Ship Ship { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
            if (id == null)
            {
                return NotFound();
            }

            var ship = _shipRepository.GetShipById((int)id);
            if (ship != null)
            {
                Ship = ship;
                _shipRepository.DeleteShip(ship.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
