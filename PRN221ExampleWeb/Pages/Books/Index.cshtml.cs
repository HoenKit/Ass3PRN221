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

namespace PRN221ExampleWeb.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookRepository _bookRepository;

        public IndexModel(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ICollection<Book> Book { get;set; } = default!;
        [BindProperty]
        public string search { get; set; }

        public async Task<IActionResult> OnGetGetBooksAsync(string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                Book =  _bookRepository.GetBooksList();
            }
            else
            {
                Book =  _bookRepository.SearchByName(search);
            }

            return new JsonResult(Book);
        }


        //public IActionResult OnPostAsync()
        //{
        //    Book = _bookRepository.SearchByName(search);
        //    return new JsonResult(Book);
        //}
    }
}
