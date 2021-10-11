using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
    }
}
