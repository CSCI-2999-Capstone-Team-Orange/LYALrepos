using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class Size
    {
        public Size()
        {
            ProductTables = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> ProductTables { get; set; }
    }
}
