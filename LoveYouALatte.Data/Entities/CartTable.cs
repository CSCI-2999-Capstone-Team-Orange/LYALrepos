using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class CartTable
    {
        public CartTable()
        {
            CartAddOnItems = new HashSet<CartAddOnItem>();
        }

        public int IdCartTable { get; set; }
        public string IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public int? CartAddOnItemId { get; set; }
        public decimal LineItemCost { get; set; }
        public decimal LineTax { get; set; }
        public decimal LineCost { get; set; }

        public virtual CartAddOnItem CartAddOnItem { get; set; }
        public virtual Product IdProductNavigation { get; set; }
        public virtual AspNetUser IdUserNavigation { get; set; }
        public virtual ICollection<CartAddOnItem> CartAddOnItems { get; set; }
    }
}
