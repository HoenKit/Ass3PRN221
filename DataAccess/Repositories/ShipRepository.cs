using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ShipRepository
    {
        public DataContext _context;
        public ShipRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateShip(Ship Ship)
        {
            _context.Ships.Add(Ship);
            _context.SaveChanges();
        }

        public void UpdateShip(Ship Ship)
        {
            _context.Ships.Update(Ship);
            _context.SaveChanges();
        }

        public void DeleteShip(int id)
        {
            if (GetShipById(id) != null)
            {
                _context.Ships.Remove(GetShipById(id));
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid Id");
            }
        }

        public Ship GetShipById(int id) => _context.Ships.Include(s => s.Book)
                .Include(s => s.UserOrder)
                .Include(s => s.UserShip).Where(c => c.Id == id).FirstOrDefault();
        public ICollection<Ship> GetShipsList() => _context.Ships.Include(s => s.Book)
                .Include(s => s.UserOrder)
                .Include(s => s.UserShip).ToList();

        public ICollection<Ship> SearchById(int id) => _context.Ships.Include(s => s.Book)
                .Include(s => s.UserOrder)
                .Include(s => s.UserShip).Where(c => c.Id == id).ToList();
    }
}
