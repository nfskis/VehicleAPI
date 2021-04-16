using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleAPI.ViewModels.Users
{
    public class LoginUserViewModel
    {

        [Required]
        [Display(Name = "type your Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        private string _password;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password
        {
            get => _password;
            set
            {
                var sha = new System.Security.Cryptography.HMACSHA512
                {
                    Key = Encoding.UTF8.GetBytes(value.Length.ToString())
                };
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                _password = Convert.ToBase64String(hash);
            }
        }

        public LoginUserViewModel()
        {

        }

        public LoginUserViewModel(string email, string password)
        {
            Email = email;
            Password = password;

        }
    }
}