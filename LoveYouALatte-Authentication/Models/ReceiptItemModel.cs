using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ReceiptItemModel
    {
        public int UserOrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductDescription { get; set;}
        public string sizeDescription { get; set; }
        public decimal unitCost { get; set; }
        public int quantity { get; set; }
        public decimal tax { get; set; }
        public decimal totalCost { get; set; }
    }
}
