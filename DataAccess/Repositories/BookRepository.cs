using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BookRepository
    {
        public DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateBook(Book Book)
        {
            _context.Books.Add(Book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book Book)
        {
            _context.Books.Update(Book);
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            if (GetBookById(id) != null)
            {
                _context.Books.Remove(GetBookById(id));
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid Id");
            }
        }

        public Book GetBookById(int id) => _context.Books.Where(c => c.Id == id).FirstOrDefault();
        public ICollection<Book> GetBooksList() => _context.Books.ToList();

        public ICollection<Book> SearchByName(string name) => _context.Books.Where(c => c.BookName.Contains(name)).ToList();
    }
}
