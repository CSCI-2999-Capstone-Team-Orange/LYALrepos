using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class CartAddOnItem
    {
        public CartAddOnItem()
        {
            AddOnItemLists = new HashSet<AddOnItemList>();
            CartTables = new HashSet<CartTable>();
        }

        public int CartAddOnItemId { get; set; }
        public int? IdCartTable { get; set; }
        public int? OrderItemId { get; set; }

        public virtual CartTable IdCartTableNavigation { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public virtual ICollection<AddOnItemList> AddOnItemLists { get; set; }
        public virtual ICollection<CartTable> CartTables { get; set; }
    }
}
