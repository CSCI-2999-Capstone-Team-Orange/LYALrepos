using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ReceiptModel
    {
        public ReceiptModel()
        {
            this.Items = new List<ReceiptItemModel>();
        }
        public string UserId { get; set; }
        public int UserOrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal GrandTotal { get; set; } 
        public List<ReceiptItemModel> Items { get; set; }
    }
}
