using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class GuestUserTable
    {
        public GuestUserTable()
        {
            GuestOrderItems = new HashSet<GuestOrderItem>();
        }

        public int IdGuestTable { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal LineItemCost { get; set; }
        public decimal LineTax { get; set; }
        public decimal LineCost { get; set; }
        public int IdGuest { get; set; }

        public virtual ICollection<GuestOrderItem> GuestOrderItems { get; set; }
    }
}
