﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class AddOnModel
    {
        public int addOnId { get; set; }
        public string addOnType { get; set; }
        public string addOnDescription { get; set; }
        public int quantity { get; set; } //raf
        public decimal addOnTotalPrice { get; set; } //raf
        public decimal addOnUnitPrice { get; set;} //raf
        public bool isSelected { get; set; }
    }
}
