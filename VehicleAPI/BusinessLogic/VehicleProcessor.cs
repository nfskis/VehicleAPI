using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;

namespace VehicleAPI.BusinessLogic
{
    public class VehicleProcessor
    {
        private readonly SqlDataAccess SqlDataAccess;

        public VehicleProcessor(IConfiguration config)
        {
            SqlDataAccess = new SqlDataAccess(config);
        }

        /// <summary>
        /// return Vehicle model
        /// </summary>
        /// <param name="value">Vehicle ID</param>
        /// <returns></returns>
        public VehicleModel FindVehicleByVehicleID(string vehicleID)
        {
            return SqlDataAccess.LoadSingleData<VehicleModel, dynamic>("dbo.Vehicle_FindVehicleByVehicleID", new { vehicleID });
        }


        /// <summary>
        /// return Vehicle model
        /// </summary>
        /// <param name="value">Vehicle ID</param>
        /// <returns></returns>
        public VehicleModel FindVehicleByPlateNumber(string plateNumber)
        {
            return SqlDataAccess.LoadSingleData<VehicleModel, dynamic>("dbo.Vehicle_FindVehicleByPlateNumber", new { plateNumber });
        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">Vehicle model</param>
        /// <returns></returns>
        public int RegisterVehicle(VehicleModel value)
        {
            value.VehicleSeqID = Guid.NewGuid().ToString();
            return SqlDataAccess.SaveData<VehicleModel, dynamic>("dbo.Vehicle_RegisterVehicle",
                                                                    new
                                                                    {
                                                                        value.VehicleSeqID,
                                                                        value.PlateNumber,
                                                                        value.Brand,
                                                                        value.Model
                                                                    });
        }
    }
}