using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;

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
        /// return Vehicle model
        /// </summary>
        /// <param name="value">Vehicle ID</param>
        /// <returns></returns>
        public List<VehicleModel> GetAllVehicles()
        {
            return SqlDataAccess.LoadData<VehicleModel, dynamic>("dbo.Vehicle_GetAllVehicles", null);
        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">Vehicle model</param>
        /// <returns></returns>
        public int RegisterVehicle(RegisterVehicleViewModel value)
        {

            var vehicle = FindVehicleByPlateNumber(value.PlateNumber);
            if (vehicle != null)
            {
                throw new Exception($@"The Vehicle already has been registery PlateNumber: {value.PlateNumber}");
            }

            return SqlDataAccess.SaveData<VehicleModel, dynamic>("dbo.Vehicle_RegisterVehicle",
                                                                    new
                                                                    {
                                                                        VehicleSeqID = Guid.NewGuid().ToString(),
                                                                        PlateNumber = value.PlateNumber,
                                                                        Brand = value.Brand,
                                                                        Model = value.Model
                                                                    });
        }
    }
}