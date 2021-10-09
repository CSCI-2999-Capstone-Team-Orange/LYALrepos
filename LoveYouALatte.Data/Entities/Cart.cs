using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public string DrinkName { get; set; }
        public string SizeName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual ProductTable IdProductNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
