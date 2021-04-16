using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleAPI.ViewModels.Users
{
    public class ResisterUserViewModel
    {
        [Required]
        [Display(Name = "type your first name")]
        [StringLength(50, ErrorMessage ="maxiumn typing legnth 50")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "type your last name")]
        [StringLength(50, ErrorMessage = "maxiumn typing legnth 50")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "type your Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password is not matched")]
        public string ConfirmPassword { get; set; }

    }

}
