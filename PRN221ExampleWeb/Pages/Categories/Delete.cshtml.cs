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

namespace PRN221ExampleWeb.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public DeleteModel(CategoryRepository categoryRepository, IHubContext<SignalRServer> hubContext)
        {
            _categoryRepository = categoryRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetCategoryById((int)id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetCategoryById((int)id);
            if (category != null)
            {
                _categoryRepository.DeleteCategory(category.Id);
                _hubContext.Clients.All.SendAsync("CategoryCreated", Category.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
