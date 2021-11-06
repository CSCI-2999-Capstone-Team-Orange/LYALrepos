using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            Drinks = new HashSet<Drink>();
        }

        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public virtual ICollection<Drink> Drinks { get; set; }
    }
}
