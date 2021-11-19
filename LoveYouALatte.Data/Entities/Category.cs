using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            DrinkFoods = new HashSet<DrinkFood>();
        }

        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public virtual ICollection<DrinkFood> DrinkFoods { get; set; }
    }
}
