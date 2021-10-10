using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Drink
    {
        public Drink()
        {
            Products = new HashSet<Product>();
        }

        public int IdDrinks { get; set; }
        public string DrinkName { get; set; }
        public string DrinkDescription { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
