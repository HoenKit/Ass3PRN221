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

namespace PRN221ExampleWeb.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(CategoryRepository categoryRepository, IHubContext<SignalRServer> hubContext)
        {
            _categoryRepository = categoryRepository;
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
        public Category Category { get; set; } = default!;

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

            _categoryRepository.CreateCategory(Category);
            _hubContext.Clients.All.SendAsync("CategoryCreated", Category.Id);

            return RedirectToPage("./Index");
        }
    }
}
