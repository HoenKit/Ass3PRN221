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

namespace PRN221ExampleWeb.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookRepository _bookRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(BookRepository bookRepository, CategoryRepository categoryRepository, IHubContext<SignalRServer> hubContext)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty]
        public ICollection<Category> Categories { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }
        public IActionResult OnGet()
        {
            Categories = _categoryRepository.GetCategoriesList();
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = _categoryRepository.GetCategoriesList();
                return Page();
            }
            int categoryId = CategoryId;
            Book.Category = _categoryRepository.GetCategoryById(categoryId);

            _bookRepository.CreateBook(Book);
            _hubContext.Clients.All.SendAsync("BookCreated", Book.Id);

            return RedirectToPage("./Index");
        }
    }
}
