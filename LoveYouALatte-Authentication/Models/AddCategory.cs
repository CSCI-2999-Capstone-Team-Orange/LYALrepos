using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class AddCategory
    {
        public string AddCategoryError { get; set; }
        public string AddCategorySuccess { get; set; }

        [Required]
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile MyImage { set; get; }
    }
}
