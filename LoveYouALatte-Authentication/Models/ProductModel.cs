using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ProductModel
    {
            public int ProductId { get; set; }
            public int DrinkId { get; set; }
            public int SizeId { get; set; }
            public decimal Price { get; set; }
      
    }
}
