using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;

namespace VehicleAPI.BusinessLogic
{
    public class VehicleProcessor
    {
        private SqlDataAccess SqlDataAccess;

        public VehicleProcessor(IConfiguration config)
        {
            SqlDataAccess = new SqlDataAccess(config);
        }

        /// <summary>
        /// return Current Vehicle location 
        /// </summary>
        /// <param name="vehicleSeqID">Target vehicle sequense ID</param>
        /// <returns></returns>
        public int RegisterVehicle(VehicleModel value)
        {
            if (string.IsNullOrWhiteSpace(value.VehicleSeqID))
                value.VehicleSeqID = Guid.NewGuid().ToString();

            string query = $@"INSERT INTO Vehicles(VehicleSeqID, PlateNumber, Brand, Model)
                            VALUES (@VehicleSeqID, @PlateNumber, @Brand, @Model)";

            return SqlDataAccess.SaveData(query, value);
        }    
    }
}