using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;

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
        public int RegisterUser(UserModel value)
        {
            bool isExisted = LoginUser(value) != null ? true : false;
            if (isExisted)
            {
                throw new Exception($"the givens user eamil existed in DataBase: '{value.Email}'");
            }

            value.UserSeqID = Guid.NewGuid().ToString();
            return SqlDataAccess.SaveData<UserModel, dynamic>("dbo.Account_RegisterUser",
                                                                new
                                                                {
                                                                    value.UserSeqID,
                                                                    value.FirstName,
                                                                    value.LastName,
                                                                    value.Email,
                                                                    value.Password,
                                                                    value.RoleID
                                                                });
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public UserModel LoginUser(UserModel value)
        {
            return SqlDataAccess.LoadSingleData<UserModel, dynamic>("dbo.Account_LoginUser",
                                                                    new
                                                                    {
                                                                        value.Email,
                                                                        value.Password
                                                                    });
        }
    }
}