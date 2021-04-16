using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;

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
            string query = @"DELETE FROM Users";
            SqlDataAccess.SingleOrDefault(query);

            query = @"DELETE FROM Vehicles";
            SqlDataAccess.SingleOrDefault(query);

            query = @"DELETE FROM UserRoles";
            SqlDataAccess.SingleOrDefault(query);

            query = @"DELETE FROM Tracks";
            SqlDataAccess.SingleOrDefault(query);
        }

        /// <summary>
        /// Set default values in the database
        /// </summary>
        /// <param name="value">User model</param>
        /// <returns>applied row count</returns>
        public async void SetDefaults()
        {
            ClearTables();

            AddUserRoles();

            AddUsers();

            await AddVechilesAsync();

        }

        private void AddUsers()
        {
            var users = new List<UserModel>()
            {
                new UserModel(){ RoleID = 1, Email="admin@gmail.com", FirstName="Insung", LastName="Kim", Password="password", UserSeqID=Guid.NewGuid().ToString()},
                new UserModel(){ RoleID = 0, Email="user@gmail.com", FirstName="Joon", LastName="Smith", Password="password", UserSeqID=Guid.NewGuid().ToString()},
                new UserModel(){ RoleID = 0, Email="mao@gmail.com", FirstName="mao", LastName="lee", Password="password", UserSeqID=Guid.NewGuid().ToString()},
                new UserModel(){ RoleID = 0, Email="Jamin.kim@gmail.com", FirstName="Jamin", LastName="Kim", Password="password", UserSeqID=Guid.NewGuid().ToString()},
                new UserModel(){ RoleID = 0, Email="dongMin.kim@gmail.com", FirstName="dongMin", LastName="Kim", Password="password", UserSeqID=Guid.NewGuid().ToString()},
                new UserModel(){ RoleID = 2, Email="Keblish@gmail.com", FirstName="Joe", LastName="Keblish", Password="password", UserSeqID=Guid.NewGuid().ToString()}
            };

            string query = @"INSERT INTO Users(UserSeqID, FirstName, lastName, email, password, RoleID)
                        VALUES(@UserSeqID, @FirstName, @lastName, @email, @password, @RoleID)";

            foreach (var curr in users)
            {
                SqlDataAccess.SaveData(query, curr);
            }
        }

        private async Task AddVechilesAsync()
        {

            string query = @"INSERT INTO Vehicles(VehicleSeqID, PlateNumber, Brand, Model, UserSeqID)
                        VALUES(@VehicleSeqID, @PlateNumber, @Brand, @Model, @userSeqID)";

            var users = SqlDataAccess.LoadData<UserModel>("Select * from Users");

            for (int i = 0; i < 1000; i++)
            {
                var vehicle = new VehicleModel()
                {
                    VehicleSeqID = Guid.NewGuid().ToString(),
                    Brand = i % 2 == 0 ? "Honda" : "Toyota",
                    Model = i % 2 == 0 ? "Camry" : "Sienna",
                    UserSeqID = users[new Random().Next(0, users.Count)].UserSeqID,
                    PlateNumber = $"{new Random(i).Next(1, 99)}-{new Random(i + 1).Next(1, 99)}{new Random(i + 2).Next(1, 9)}{new Random(i + 3).Next(1, 9)}",

                };
                await SqlDataAccess.SaveDataAsync(query, vehicle);
            }
        }

        private int AddUserRoles()
        {
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
