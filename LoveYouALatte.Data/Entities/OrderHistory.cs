using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class OrderHistory
    {
        public int IdOrderHistory { get; set; }
        public string IdUser { get; set; }
        public int IdCartTable { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime PurchaseTime { get; set; }

        public virtual CartTable IdCartTableNavigation { get; set; }
        public virtual AspNetUser IdUserNavigation { get; set; }
    }
}
