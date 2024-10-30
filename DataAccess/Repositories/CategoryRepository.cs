using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoryRepository
    {
        public DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
                _context.Categories.Update(category);
                _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            if (GetCategoryById(id) != null)
            {
                _context.Categories.Remove(GetCategoryById(id));
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid Id");
            }
        }

        public Category GetCategoryById(int id) => _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        public ICollection<Category> GetCategoriesList() => _context.Categories.ToList();

        public ICollection<Category> SearchByName(string name) => _context.Categories.Where(c => c.CategoryName.Contains(name)).ToList();
    }
}
