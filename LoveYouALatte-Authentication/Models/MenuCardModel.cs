using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class MenuCardModel
    {
        public MenuCardModel()

        {
            this.drinkList= new List<DrinkModel>();
            this.MenuCardProductList = new List<ProductModel>();
        }
       
        
        public List<DrinkModel> drinkList { get; set; }
        public List<ProductModel> MenuCardProductList { get; set; }

    }
}
