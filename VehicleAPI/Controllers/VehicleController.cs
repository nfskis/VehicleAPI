using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleAPI.BusinessLogic;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;
using VehicleAPI.ViewModels.Vehicles;

namespace VehicleAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private VehicleProcessor VehicleProcessor;

        public VehicleController(IConfiguration config)
        {
            VehicleProcessor = new VehicleProcessor(config);
        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">VehicleModel</param>
        [HttpPost]
        [Route("api/Vehicle")]
        [Authorize(Roles = "User, Admin")]
        public ActionResult RegisterVehicle([FromForm] RegisterVehicleViewModel value)
        {
            string userSeqID = User.Claims.First().Value;
            
            VehicleProcessor.RegisterVehicle(value, userSeqID);
            return Ok("Resister Vehicle has been sucessful");
        }
        
        /// <summary>
        /// get all vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/vehicle")]
        [Authorize(Roles = "Admin")]
        public List<VehicleModel> Get()
        {
            return VehicleProcessor.GetAllVehicles();
        }

        [HttpPut]
        [Route("api/vehicle")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateVehicle([FromForm] UpdateVehicleViewModel updateVehicle)
        {
            var result = VehicleProcessor.UpdatVehicle(updateVehicle);
            if (result >= 0)
                return Ok($"Update Vehicle: {updateVehicle.PlateNumber}");
            else
                return BadRequest($"Failed Update Vehicle: {updateVehicle.PlateNumber}");
        }

        [HttpDelete]
        [Route("api/vehicle")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteVehicle([FromForm] DeleteVehicleViewModel deleteVehicle)
        {
            var result = VehicleProcessor.DeleteVehicle(deleteVehicle);
            if (result >= 0)
                return Ok($"Delete Vehicle: {deleteVehicle.PlateNumber}");
            else
                return BadRequest($"Failed delete Vehicle: {deleteVehicle.PlateNumber}");

        }

        /// <summary>
        /// search Vehicle by plate number
        /// </summary>
        /// <param name="value"></param>
        [HttpGet]
        [Route("api/vehicle/plateNumber")]
        [Authorize(Roles = "Admin")]
        public VehicleModel SearchbyPlateNumber([FromHeader] string plateNumber)
        {
            var result = VehicleProcessor.FindVehicleByPlateNumber(plateNumber);
            if (result == null)
                return new VehicleModel();
            else
                return result;
        }

    }
}
