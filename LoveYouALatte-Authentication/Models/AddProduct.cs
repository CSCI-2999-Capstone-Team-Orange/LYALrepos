using FoolProof.Core;
using Microsoft.AspNetCore.Http;
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

        [RequiredIf("CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Small Price is required.")]
        [Display(Name = "Small Price")]
        [RegularExpressionIf(@"^[0-9]+(\.[0-9]{1,2})$", "CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Valid small price with a maximum of 2 decimal places is required.")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid price with a maximum of 2 decimal places is required.")]
        public decimal SmallPrice { get; set; }

        [RequiredIf("CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Medium Price is required.")]
        [Display(Name = "Medium Price")]
        [RegularExpressionIf(@"^[0-9]+(\.[0-9]{1,2})$", "CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Valid medium price with a maximum of 2 decimal places is required.")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid price with a maximum of 2 decimal places is required.")]
        public decimal MediumPrice { get; set; }

        [RequiredIf("CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Large Price is required.")]
        [Display(Name = "Large Price")]
        [RegularExpressionIf(@"^[0-9]+(\.[0-9]{1,2})$", "CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Valid large price with a maximum of 2 decimal places is required.")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid price with a maximum of 2 decimal places is required.")]
        public decimal LargePrice { get; set; }

        [RequiredIf("CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Small SKU is required.")]
        [Display(Name = "Small SKU")]
        public string SmallSKU { get; set; }

        [RequiredIf("CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Medium SKU is required.")]
        [Display(Name = "Medium SKU")]
        public string MediumSKU { get; set; }

        [RequiredIf("CategoryID", Operator.NotEqualTo, 5, ErrorMessage = "Large SKU is required.")]
        [Display(Name = "Large SKU")]
        public string LargeSKU { get; set; }

        [RequiredIf("CategoryID", Operator.EqualTo, 5, ErrorMessage = "Item Price is required.")]
        [Display(Name = "Item Price")]
        [RegularExpressionIf(@"^[0-9]+(\.[0-9]{1,2})$", "CategoryID", Operator.EqualTo, 5, ErrorMessage = "Valid item price with a maximum of 2 decimal places is required.")]
        public decimal ItemPrice { get; set; }

        [RequiredIf("CategoryID", Operator.EqualTo, 5, ErrorMessage = "Item SKU is required.")]
        [Display(Name = "Large SKU")]
        public string ItemSKU { get; set; }
        [Required]
        [Display(Name = "Item Description")]
        public string DrinkDescription { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string DrinkName { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public CategoryModel categoryDivID { get; set; }
        //[Required]
        [Display(Name = "Upload Image")]
        public IFormFile MyImage { set; get; }

    }
}
