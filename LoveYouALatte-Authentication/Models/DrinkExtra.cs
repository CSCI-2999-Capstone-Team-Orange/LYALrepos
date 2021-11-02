using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class DrinkExtra
    {
        public DrinkExtra()
        {
            ProductTables = new HashSet<ProductKG>();
        }

        public int DrinkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductKG> ProductTables { get; set; }
    }
}
