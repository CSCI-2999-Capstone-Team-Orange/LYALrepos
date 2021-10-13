using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class CartTable
    {
        public int IdCartTable { get; set; }
        public string IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal LineItemCost { get; set; }

        public virtual Product IdProductNavigation { get; set; }
        public virtual AspNetUser IdUserNavigation { get; set; }
    }
}
