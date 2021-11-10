using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Categories = new List<CategoryModel>();
        }
        public List<CategoryModel> Categories { get; set; }
    }
}
