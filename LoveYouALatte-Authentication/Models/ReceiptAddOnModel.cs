using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ReceiptAddOnModel
    {
        public string addOnType { get; set; }
        public string addOnDescription { get; set; }
        public int quantity { get; set; }
        public decimal addOnUnitPrice { get; set; }

    }
}
