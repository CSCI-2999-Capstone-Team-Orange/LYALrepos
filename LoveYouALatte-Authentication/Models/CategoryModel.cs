using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            this.categoryIdList = new List<CategoryModel>();
        }

        public List<CategoryModel> categoryIdList { get; set; }
        public int IdDrinks { get; set; }
        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string DrinkName { get; set; }
        public string DrinkDescription { get; set; }
    }
}
