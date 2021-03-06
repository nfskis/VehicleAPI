using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;
using VehicleAPI.ViewModels.Vehicles;

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
        /// return Vehicle models
        /// </summary>
        /// <param name="value">Vehicle ID</param>
        /// <returns></returns>
        public async Task<List<VehicleModel>> GetAllVehiclesAsync()
        {
            return (List<VehicleModel>)await SqlDataAccess.LoadDataAsync<VehicleModel>("dbo.Vehicle_GetAllVehicles");
        }

        /// <summary>
        /// return Vehicle models
        /// </summary>
        /// <returns></returns>
        public List<VehicleModel> GetAllVehicles(int pageNumber, int rowsOfPage)
        {
            return SqlDataAccess.LoadData<VehicleModel, dynamic>("dbo.Vehicle_GetPage", new { pageNumber, rowsOfPage });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateVehicle"></param>
        internal int UpdatVehicle(UpdateVehicleViewModel updateVehicle)
        {
            return SqlDataAccess.StoredProcesdure("dbo.UpdateVehicle", updateVehicle);
        }

        /// <summary>
        /// delete Vehicle
        /// </summary>
        /// <param name="deleteVehicle">Delete Vehicle View Model</param>
        public int DeleteVehicle(DeleteVehicleViewModel deleteVehicle)
        {
            return SqlDataAccess.StoredProcesdure("dbo.deleteVehicle", deleteVehicle);
        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">Vehicle model</param>
        /// <returns></returns>
        public int RegisterVehicle(RegisterVehicleViewModel value, string userSeqID)
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
                                                                        UserSeqID = userSeqID,
                                                                        PlateNumber = value.PlateNumber,
                                                                        Brand = value.Brand,
                                                                        Model = value.Model
                                                                    });
        }
    }
}