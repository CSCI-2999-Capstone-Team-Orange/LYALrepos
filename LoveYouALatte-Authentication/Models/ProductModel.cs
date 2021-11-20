using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ProductModel
    {

        public ProductModel()
        {
            this.productList = new List<ProductModel>();
            this.categoryIdList = new List<CategoryModel>();

        }

        public List<CategoryModel> categoryIdList { get; set; }
        [Required]
        [Range(1,1000)]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 1000)]
        public int DrinkId { get; set; }

        [Required]
        //[Range(1, 3, ErrorMessage = "This size Id must be a value from 1-3.")]
        public int SizeId { get; set; }

        [Required(ErrorMessage = "Product SKU is required and cannot be longer than 15 characters.")]
        [StringLength (15)]
        public string ProductSku { get; set;}

        [Required(ErrorMessage = "Product category is required and cannot be longer than 15 characters.")]
        //[Range(1, 3, ErrorMessage = "Product category must be already listed")]
        public int category { get; set; }

        public string CategoryName { get; set; }

        [Required (ErrorMessage = "A price is required.")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage ="The price cannot be below zero and must only contain two decimal places.")]
        public decimal Price { get; set; }

        [Required (ErrorMessage = "A production description is required.")]
        [StringLength(200)]
        public string DrinkDescription { get; set; }

        [Required(ErrorMessage = "A drink name is required.")]
        [StringLength(45)]
        public string DrinkName { get; set; }

        [Required]
        [StringLength(255)]
        public string SizeName { get; set; }

        public int cartAddOnItemId { get; set; }

        public List<ProductModel> productList { get; set; }

    }
}
