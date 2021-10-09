using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Drink
    {
        public Drink()
        {
            ProductTables = new HashSet<ProductTable>();
        }

        public int DrinkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductTable> ProductTables { get; set; }
    }
}
