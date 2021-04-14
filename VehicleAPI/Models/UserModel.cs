using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VehicleAPI.Models
{
    public class UserModel
    {
        public string UserSeqID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

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




    }
}