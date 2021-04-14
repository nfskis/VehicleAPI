using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;

namespace VehicleAPI.BusinessLogic
{
    public class SetDefaultProcessor
    {
        private SqlDataAccess SqlDataAccess;

        public SetDefaultProcessor(IConfiguration config)
        {
            SqlDataAccess = new SqlDataAccess(config);
        }

        public void ClearTables()
        {
            string query = @"DELETE FROM UserRoles";
            SqlDataAccess.SingleOrDefault(query);
        }

        /// <summary>
        /// Set default values in the database
        /// </summary>
        /// <param name="value">User model</param>
        /// <returns>applied row count</returns>
        public int SetDefaults()
        {
            ClearTables();

            // set default RoleModels.
            int result = 0;
            var userRoles = new List<UserRoleModel>() {
                new UserRoleModel { RoleID = 0, TypeName = "User", RoleGroup = "UsersGroup" },
                new UserRoleModel { RoleID = 1, TypeName = "Admin", RoleGroup = "AdministratorsGroup" },
                new UserRoleModel { RoleID = 2, TypeName = "Operator", RoleGroup = "AdministratorsGroup" }
            };

            string query = @"INSERT INTO UserRoles(RoleID, TypeName, RoleGroup)
                        VALUES(@RoleID, @TypeName, @RoleGroup)";

            foreach (var curr in userRoles)
            {
                result += SqlDataAccess.SaveData(query, curr);
            }
            return result;
        }


    }
}
