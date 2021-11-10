using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class AddProduct
    {
        public AddProduct()
        {
            categoryDivID = new CategoryModel();
        }
        public string AddProductError { get; set; }
        public string AddProductSuccess { get; set; }

        [Required]
        [Display(Name = "Small Price")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid price with a maximum of 2 decimal places is required.")]
        public decimal SmallPrice { get; set; }

        [Required]
        [Display(Name = "Medium Price")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid price with a maximum of 2 decimal places is required.")]
        public decimal MediumPrice { get; set; }

        [Required]
        [Display(Name = "Large Price")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid price with a maximum of 2 decimal places is required.")]
        public decimal LargePrice { get; set; }

        [Required]
        [Display(Name = "Small SKU")]
        public string SmallSKU { get; set; }

        [Required]
        [Display(Name = "Medium SKU")]
        public string MediumSKU { get; set; }

        [Required]
        [Display(Name = "Large SKU")]
        public string LargeSKU { get; set; }

        [Required]
        [Display(Name = "Drink Description")]
        public string DrinkDescription { get; set; }

        [Required]
        [Display(Name = "Drink Name")]
        public string DrinkName { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public int CategoryID { get; set; }

        public CategoryModel categoryDivID { get; set; }

    }
}
