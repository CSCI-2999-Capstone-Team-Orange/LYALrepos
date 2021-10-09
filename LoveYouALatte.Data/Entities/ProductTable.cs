using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class ProductTable
    {
        public ProductTable()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public int DrinkId { get; set; }
        public int SizeId { get; set; }
        public decimal Price { get; set; }

        public virtual Drink Drink { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
