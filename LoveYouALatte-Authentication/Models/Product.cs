using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int DrinkId { get; set; }
        public int SizeId { get; set; }
        public decimal Price { get; set; }
        public string DrinkDescription { get; set; }
        public string DrinkName { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
    }
}
