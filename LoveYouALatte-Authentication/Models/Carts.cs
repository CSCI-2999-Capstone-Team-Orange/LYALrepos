using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class Carts
    {
        public Carts()
        {
            CartsList = new List<Cart>();
        }
        public List<Cart> CartsList { get; set; }
    }
}
