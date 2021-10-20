using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public string InvName { get; set; }
        public string InvSize { get; set; }
        public int? InvQuantity { get; set; }
        public decimal? InvPrice { get; set; }
        public string InvDescription { get; set; }
    }
}
