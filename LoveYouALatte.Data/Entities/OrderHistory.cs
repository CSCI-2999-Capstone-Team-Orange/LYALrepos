using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class OrderHistory
    {
        public int OrderId { get; set; }
        public double CartId { get; set; }
        public double UserId { get; set; }
        public string ItemName { get; set; }
        public string Size { get; set; }
    }
}
