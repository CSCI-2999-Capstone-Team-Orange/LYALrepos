using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Size
    {
        public Size()
        {
            Products = new HashSet<Product>();
        }

        public int IdSize { get; set; }
        public int Size1 { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
