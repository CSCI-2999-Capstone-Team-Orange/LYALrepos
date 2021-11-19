using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            CartTables = new HashSet<CartTable>();
        }

        public int IdProduct { get; set; }
        public int IdDrinkFood { get; set; }
        public int? IdSize { get; set; }
        public string ProductSku { get; set; }
        public decimal Price { get; set; }

        public virtual DrinkFood IdDrinkFoodNavigation { get; set; }
        public virtual Size IdSizeNavigation { get; set; }
        public virtual ICollection<CartTable> CartTables { get; set; }
    }
}
