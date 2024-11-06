using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using Microsoft.AspNetCore.SignalR;
using DataAccess.Repositories;
using PRN221ExampleWeb.Hubs;

namespace PRN221ExampleWeb.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookRepository _bookRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public DeleteModel(BookRepository bookRepository, IHubContext<SignalRServer> hubContext)
        {
            _bookRepository = bookRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookRepository.GetBookById((int) id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookRepository.GetBookById((int)id);
            if (book != null)
            {
                Book = book;
                _bookRepository.DeleteBook(book.Id);
                _hubContext.Clients.All.SendAsync("BookCreated", Book.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
