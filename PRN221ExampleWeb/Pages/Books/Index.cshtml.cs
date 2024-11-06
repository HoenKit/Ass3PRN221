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

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public IActionResult OnGetGetBooksAsync()
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
                Book = _bookRepository.SearchByName(Search);
            }
            else
            {
                Book = _bookRepository.GetBooksList();
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
