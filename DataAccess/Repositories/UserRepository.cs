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
    }
}
