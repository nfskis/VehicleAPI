using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;

namespace VehicleAPI.BusinessLogic
{
    public class LoginProcessor
    {
        private SqlDataAccess SqlDataAccess;

        public LoginProcessor(IConfiguration config)
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

            if (string.IsNullOrWhiteSpace(value.UserSeqID))
                value.UserSeqID = Guid.NewGuid().ToString();

            string query = @"INSERT INTO Users(UserSeqID, FirstName, LastName, Email, Password, RoleID)
                        VALUES(@UserSeqID, @FirstName, @LastName, @Email, @Password, @RoleID)";

            return SqlDataAccess.SaveData(query, value);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public UserModel LoginUser(UserModel value)
        {
            string query = $@"SELECT UserSeqID
				                            ,FirstName
				                            ,LastName
				                            ,Email, 
				                            UserRoles.TypeName as Role		
                             FROM   Users, UserRoles
                             WHERE  users.RoleID = UserRoles.RoleID AND
                                    Email = '{value.Email}' AND password='{value.Password}'";
            return SqlDataAccess.SingleOrDefault<UserModel>(query);
        }
    }
}