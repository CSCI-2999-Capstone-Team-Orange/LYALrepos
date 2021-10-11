﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public string DrinkName { get; set; }
        public string SizeName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
