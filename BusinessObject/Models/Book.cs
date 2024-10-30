using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
    }
}
