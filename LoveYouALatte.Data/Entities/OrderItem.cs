using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int UserOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal LineItemCost { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalCost { get; set; }

        public virtual UserOrder UserOrder { get; set; }
    }
}
