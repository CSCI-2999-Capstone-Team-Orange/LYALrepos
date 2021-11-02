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
            ProductTables = new HashSet<ProductKG>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductKG> ProductTables { get; set; }
    }
}
