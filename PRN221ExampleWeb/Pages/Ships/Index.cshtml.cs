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

        [BindProperty]
        public string search { get; set; }

        public async Task OnGetAsync()
        {
            Ship = _shipRepository.GetShipsList();
        }

        public async Task OnPostAsync()
        {
            int id = int.Parse(search);
            Ship = _shipRepository.SearchById(id);
        }
    }
}
