using System;
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
        public bool isSelected { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
