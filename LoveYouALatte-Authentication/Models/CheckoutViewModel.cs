using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel()
        {
            Carts = new List<Cart>();
        }
        public List<Cart> Carts { get; set; }
    }
}
