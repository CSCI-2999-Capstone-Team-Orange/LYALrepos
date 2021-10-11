using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class Drink
    {
        public Drink()
        {
            ProductTables = new HashSet<Product>();
        }

        public int DrinkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> ProductTables { get; set; }
    }
}
