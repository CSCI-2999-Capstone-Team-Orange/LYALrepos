using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class OrderHistory
    {
        public int IdOrderHistory { get; set; }
        public int IdCartTable { get; set; }
        public int IdUser { get; set; }
        public byte Purchased { get; set; }
        public DateTime Time { get; set; }
        public int CartTableIdCartTable { get; set; }
        public int CartTableIdProduct { get; set; }
        public int CartTableIdUser { get; set; }
    }
}
