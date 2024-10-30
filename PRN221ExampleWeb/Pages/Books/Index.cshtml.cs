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

        public async Task OnGetAsync()
        {
            Book = _bookRepository.GetBooksList();
        }
    }
}
