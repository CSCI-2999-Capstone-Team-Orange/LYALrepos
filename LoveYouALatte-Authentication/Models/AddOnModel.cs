using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class AddOnModel
    {
        [Required(ErrorMessage = "AddOn Id is required.")]
        public int addOnId { get; set; }
        public string addOnType { get; set; }
        public string addOnDescription { get; set; }
        public bool isSelected { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Value can't be below 0 and above 20.")]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
