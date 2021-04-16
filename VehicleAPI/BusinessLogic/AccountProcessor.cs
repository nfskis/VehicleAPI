using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;
using VehicleAPI.ViewModels.Users;

namespace VehicleAPI.BusinessLogic
{
    public class AccountProcessor
    {
        private readonly SqlDataAccess SqlDataAccess;

        public AccountProcessor(IConfiguration config)
        {
            SqlDataAccess = new SqlDataAccess(config);
        }


        /// <summary>
        /// encoding password 
        /// </summary>
        /// <param name="password">password</param>
        /// <returns></returns>
        private string EncodingPassword(string password)
        {
            var sha = new System.Security.Cryptography.HMACSHA512
            {
                Key = Encoding.UTF8.GetBytes(password.Length.ToString())
            };
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="value">User model</param>
        /// <returns>applied row count</returns>
        public int RegisterUser(ResisterUserViewModel value)
        {

            return SqlDataAccess.SaveData<UserModel, dynamic>("dbo.Account_RegisterUser",
                                                                new
                                                                {
                                                                    UserSeqID = Guid.NewGuid().ToString(),
                                                                    FirstName = value.FirstName,
                                                                    LastName = value.LastName,
                                                                    Email = value.Email,
                                                                    Password = EncodingPassword(value.Password),
                                                                    RoleID = 0, // default set as an user
                                                                });
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="value">User view model</param>
        public void UpdateUser(string email, UpdateUserViewModel value)
        {
            SqlDataAccess.SingleOrDefault<dynamic>("dbo.Account_UpdateUser",
                                                    new
                                                    {
                                                        FirstName = value.FirstName,
                                                        LastName = value.LastName,
                                                        Email = email,
                                                        Password = EncodingPassword(value.Password)
                                                    });
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userId">User ID</param>
        public void DeleteUser(string email)
        {
            SqlDataAccess.SingleOrDefault<dynamic>("dbo.Account_DeleteUser", new { email });
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="value">Login User View Model</param>
        /// <returns></returns>
        public UserModel LoginUser(LoginUserViewModel value)
        {
            return SqlDataAccess.LoadSingleData<UserModel, dynamic>("dbo.Account_LoginUser",
                                                                    new
                                                                    {
                                                                        value.Email,
                                                                        value.Password
                                                                    });
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        internal List<GetUsersViewModel> GetUsers()
        {
            return SqlDataAccess.LoadData<GetUsersViewModel, dynamic>("dbo.Account_GetUsers", new { });
        }
    }
}