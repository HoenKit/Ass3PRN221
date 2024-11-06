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

namespace PRN221ExampleWeb.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CategoryRepository _categoryRepository;

        public IndexModel(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICollection<Category> Category { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public IActionResult OnGetGetCategoriesAsync()
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
                Category = _categoryRepository.SearchByName(Search);
            }
            else
            {
                Category = _categoryRepository.GetCategoriesList();
            }

            return new JsonResult(Category);
        }
    }
}
