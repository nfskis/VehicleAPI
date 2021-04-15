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
        [Route("api/Vehicle/register/")]
        [Authorize(Roles = "User, Admin")]
        public ApiResultMessage RegisterVehicle([FromForm] RegisterVehicleViewModel value)
        {
            try
            {
                value.UserSeqID = User.Claims.First().Value;
                VehicleProcessor.RegisterVehicle(value);
                return new ApiResultMessage("Resister Vehicle has been sucessful", "Success");
            }
            catch (Exception)
            {
                return new ApiResultMessage("Resister Vehicle has been Failed", "Failed");
            }
        }

        /// <summary>
        /// search Vehicle by plate number
        /// </summary>
        /// <param name="value"></param>
        [HttpGet]
        [Route("api/vehicle/search/")]
        [Authorize(Roles = "Admin")]
        public VehicleModel SearchbyPlateNumber([FromHeader] string PlateNumber)
        {
            return VehicleProcessor.FindVehicleByPlateNumber(PlateNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpGet]
        [Route("api/vehicle/all/")]
        [Authorize(Roles = "Admin")]
        public List<VehicleModel> all()
        {
            return VehicleProcessor.GetAllVehicles();
        }


    }
}
