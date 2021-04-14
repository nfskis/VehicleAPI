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
        /// https://localhost:44309/api/vehicle/register/
        /// </summary>
        /// <param name="value">VehicleModel</param>
        [Route("api/Vehicle/register/")]
        [HttpPost]
        public void Post([FromBody] VehicleModel value)
        {
            VehicleProcessor.RegisterVehicle(value);
        }


    }
}
