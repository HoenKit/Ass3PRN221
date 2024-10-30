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

namespace PRN221ExampleWeb.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BookRepository _bookRepository;
        private readonly CategoryRepository _categoryRepository;

        public EditModel(BookRepository bookRepository, CategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty]
        public ICollection<Category> Categories { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  _bookRepository.GetBookById((int)id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            CategoryId = book.Category.Id;
            Categories = _categoryRepository.GetCategoriesList();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _categoryRepository.GetCategoriesList();
                return Page();
            }
            Book.Category = _categoryRepository.GetCategoryById(CategoryId);
            _bookRepository.UpdateBook(Book);

            return RedirectToPage("./Index");
        }
    }
}
