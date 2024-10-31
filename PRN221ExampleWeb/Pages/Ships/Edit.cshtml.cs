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

namespace PRN221ExampleWeb.Pages.Ships
{
    public class EditModel : PageModel
    {
        private readonly ShipRepository _shipRepository;
        private readonly UserRepository _userRepository;
        private readonly BookRepository _bookRepository;

        public EditModel(ShipRepository shipRepository, UserRepository userRepository, BookRepository bookRepository)
        {
            _shipRepository = shipRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Ship Ship { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ship =  _shipRepository.GetShipById((int)id);
            if (ship == null)
            {
                return NotFound();
            }
            Ship = ship;
           ViewData["BookId"] = new SelectList(_bookRepository.GetBooksList(), "Id", "BookName");
           ViewData["UserOrderId"] = new SelectList(_userRepository.GetUsersList(), "Id", "UserName");
           ViewData["UserShipId"] = new SelectList(_userRepository.GetUsersList(), "Id", "UserName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _shipRepository.UpdateShip(Ship);

            return RedirectToPage("./Index");
        }
    }
}
