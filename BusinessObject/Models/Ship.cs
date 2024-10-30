using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateShip { get; set; }
        public int UserOrderId { get; set; }
        public User? UserOrder { get; set; }
        public int UserShipId { get; set; }
        public User? UserShip { get; set; }
    }
}
