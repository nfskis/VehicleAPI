using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;

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
        /// Register User
        /// </summary>
        /// <param name="value">User model</param>
        /// <returns>applied row count</returns>
        public int RegisterUser(UserViewModel value)
        {
            var user = new LoginUserViewModel(value.Email, value.Password);
            bool isExisted = LoginUser(user) != null ? true : false;
            if (isExisted)
            {
                throw new Exception($"the givens user eamil existed in DataBase: '{value.Email}'");
            }

            return SqlDataAccess.SaveData<UserModel, dynamic>("dbo.Account_RegisterUser",
                                                                new
                                                                {
                                                                    UserSeqID = Guid.NewGuid().ToString(),
                                                                    FirstName = value.FirstName,
                                                                    LastName = value.LastName,
                                                                    Email = value.Email,
                                                                    Password = user.Password,
                                                                    RoleID = value.RoleID,
                                                                });
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="value">User view model</param>
        public void UpdateUser(UpdateUserViewModel value)
        {
            SqlDataAccess.LoadSingleData<UpdateUserViewModel, dynamic>("dbo.Account_UpdateUser", value);
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userId">User ID</param>
        public void DeleteUser(string userSeqID)
        {
            SqlDataAccess.LoadSingleData<UserViewModel, dynamic>("dbo.Account_DeleteUser", new { userSeqID });
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
        internal List<UserViewModel> GetUsers()
        {
            return SqlDataAccess.LoadData<UserViewModel, dynamic>("dbo.Account_GetUsers", new { });
        }
    }
}