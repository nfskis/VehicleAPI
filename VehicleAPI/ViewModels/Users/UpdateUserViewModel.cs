using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleAPI.ViewModels.Users
{
    public class UpdateUserViewModel
    {
        [Required]
        [Display(Name = "type your first name")]
        [StringLength(50, ErrorMessage = "maxiumn typing legnth 50")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "type your last name")]
        [StringLength(50, ErrorMessage = "maxiumn typing legnth 50")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Confirm Password")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password is not matched")]
        public string ConfirmPassword { get; set; }


    }

}
