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
        }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int DrinkId { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "This price cannot be below zero")]
        public int SizeId { get; set; }
        [Required]
        [Range(.01,1000000000, ErrorMessage ="This price cannot be below zero")]
        public decimal Price { get; set; }
        [Required]
        public string DrinkDescription { get; set; }
        [Required]
        public string DrinkName { get; set; }
        [Required]
        public string SizeName { get; set; }
      

        public List<ProductModel> productList { get; set; }

    }
}
