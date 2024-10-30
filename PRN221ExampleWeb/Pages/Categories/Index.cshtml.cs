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

        public async Task OnGetAsync()
        {
            Category = _categoryRepository.GetCategoriesList();
        }
    }
}
