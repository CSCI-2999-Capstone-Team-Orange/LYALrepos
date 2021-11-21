using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class AddOnModel
    {
        public int addOnId { get; set; }
        public string addOnType { get; set; }
        public string addOnDescription { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        public bool isSelected { get; set; }
    }
}
