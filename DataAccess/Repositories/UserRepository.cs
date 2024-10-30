using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        public DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool Login (string username, string password)
        {
            var user = _context.Users.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateUser(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
        }

        public void UpdateUser(User User)
        {
            _context.Users.Update(User);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            if (GetUserById(id) != null)
            {
                _context.Users.Remove(GetUserById(id));
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid Id");
            }
        }

        public User GetUserById(int id) => _context.Users.Where(c => c.Id == id).FirstOrDefault();
        public ICollection<User> GetUsersList() => _context.Users.ToList();

        public ICollection<User> SearchByName(string name) => _context.Users.Where(c => c.UserName.Contains(name)).ToList();
    }
}
