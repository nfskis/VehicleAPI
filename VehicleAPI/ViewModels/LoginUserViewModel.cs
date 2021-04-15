using System;
using System.Text;

namespace VehicleAPI.ViewModels
{
    public class LoginUserViewModel
    {
        public string Email { get; set; }
        private string _password;
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