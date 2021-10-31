using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class DrinkModel
    {
        public DrinkModel()
        {
            this.drinkIdList = new List<DrinkModel>();
        }
        public int drinkId { get; set; }
        public string drinkName { get; set; }

        public List<DrinkModel> drinkIdList { get; set; }
    }
}
