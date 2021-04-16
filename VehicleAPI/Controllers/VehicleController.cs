using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using VehicleAPI.BusinessLogic;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;

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
            try
            {
                value.UserSeqID = User.Claims.First().Value;
                VehicleProcessor.RegisterVehicle(value);
                return Ok("Resister Vehicle has been sucessful");
            }
            catch (Exception)
            {
                return BadRequest("Resister Vehicle has been Failed");
            }
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
        public void UpdateVehicle()
        {
            ///*return */VehicleProcessor.GetAllVehicles();
        }

        [HttpDelete]
        [Route("api/vehicle")]
        [Authorize(Roles = "Admin")]
        public void DeleteVehicle()
        {
            //return VehicleProcessor.GetAllVehicles();
        }

        /// <summary>
        /// search Vehicle by plate number
        /// </summary>
        /// <param name="value"></param>
        [HttpGet]
        [Route("api/vehicle/search")]
        [Authorize(Roles = "Admin")]
        public VehicleModel SearchbyPlateNumber([FromHeader] string PlateNumber)
        {
            return VehicleProcessor.FindVehicleByPlateNumber(PlateNumber);
        }

    }
}
