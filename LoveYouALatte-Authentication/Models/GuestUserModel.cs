using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class GuestUserModel
    {
        [Required(ErrorMessage = "Your first name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
