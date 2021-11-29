using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class DrinkFood
    {
        public DrinkFood()
        {
            Products = new HashSet<Product>();
        }

        public int IdDrinkFood { get; set; }
        public int? IdCategory { get; set; }
        public string DrinkName { get; set; }
        public string DrinkDescription { get; set; }
        public int Inventory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
