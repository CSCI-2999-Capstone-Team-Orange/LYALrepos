using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Size
    {
        public Size()
        {
            ProductTables = new HashSet<ProductTable>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductTable> ProductTables { get; set; }
    }
}
