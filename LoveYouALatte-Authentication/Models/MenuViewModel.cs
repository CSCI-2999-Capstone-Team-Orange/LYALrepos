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
            Products = new List<ProductKG>();
        }
        public List<ProductKG> Products { get; set; }

        public ProductModel updateProduct { get; set; }

        public DrinkModel drinkDivID { get; set; }

        public ViewAddOnModel addOns { get; set; }

        public CheckoutItemModel checkoutItems { get; set; }
       
    }
}
