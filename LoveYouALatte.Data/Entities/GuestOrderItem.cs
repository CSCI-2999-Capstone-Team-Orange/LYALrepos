using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class GuestOrderItem
    {
        public int GuestOrderItemId { get; set; }
        public int IdGuestTable { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal LineItemCost { get; set; }
        public decimal LineTax { get; set; }
        public decimal TotalCost { get; set; }

        public virtual GuestUserTable IdGuestTableNavigation { get; set; }
    }
}
