using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }
    }
}
