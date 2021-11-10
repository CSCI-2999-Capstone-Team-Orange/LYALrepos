using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class CheckoutProdctAddOnModel
    {

        public int cartAddOnItemId { get; set; }
        public List<ReceiptAddOnModel> addOnList { get; set; }
    }
}
