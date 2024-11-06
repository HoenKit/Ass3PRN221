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

namespace PRN221ExampleWeb.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserRepository _userRepository;

        public IndexModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<User> User { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public IActionResult OnGetGetUsersAsync()
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
                User = _userRepository.SearchByName(Search);
            }
            else
            {
                User = _userRepository.GetUsersList();
            }

            return new JsonResult(User);
        }
    }
}
