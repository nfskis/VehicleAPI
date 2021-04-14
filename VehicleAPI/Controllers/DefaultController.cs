using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleAPI.BusinessLogic;
using VehicleAPI.Models;

namespace VehicleAPI.Controllers
{
    [ApiController]
    public class DefaultController : Controller
    {
        private SetDefaultProcessor SetDefaultProcessor;

        public DefaultController(IConfiguration config)
        {
            SetDefaultProcessor = new SetDefaultProcessor(config);
        }

        /// <summary>
        /// Register Vehicle
        /// https://localhost:44309/api/vehicle/register/
        /// </summary>
        /// <param name="value">VehicleModel</param>
        [Route("api/Vehicle/Default/")]
        [HttpPost]
        public int SetDefaultRoles()
        {
            return SetDefaultProcessor.SetDefaults();
        }


    }
}
