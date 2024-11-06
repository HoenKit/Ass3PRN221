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
    public class IndexModel : PageModel
    {
        private readonly ShipRepository _shipRepository;

        public IndexModel(ShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public ICollection<Ship> Ship { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public IActionResult OnGetGetShipsAsync()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return new JsonResult(new { success = false, message = "Unauthorized. Please log in." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            if (!string.IsNullOrEmpty(Search))
            {
                Ship = _shipRepository.SearchById(int.Parse(Search));
            }
            else
            {
                Ship = _shipRepository.GetShipsList();
            }

            return new JsonResult(Ship);
        }

        
    }
}
