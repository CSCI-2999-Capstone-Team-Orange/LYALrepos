using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ManageMenuTableModel
    {
        public ProductModel viewProduct { get; set; }

        public ProductModel updateProduct { get; set;}

        public List<SelectListItem> DrinkCategories { get; set; }


        
    }
}
