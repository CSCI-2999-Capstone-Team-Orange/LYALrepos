using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Product
    {
        public int ProductsId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
    }
}
