using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }//quantity * price
        public decimal LineTax { get; set; }
        public decimal LineCost { get; set; }
        public string SizeName { get; set; }
        public string DrinkName { get; set; }

        public int cartAddOnItemId { get; set; }
    }
}
