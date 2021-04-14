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
        private readonly SetDefaultProcessor SetDefaultProcessor;
        private readonly TrackProcessor TrackProcessor;

        public DefaultController(IConfiguration config)
        {
            SetDefaultProcessor = new SetDefaultProcessor(config);
            TrackProcessor = new TrackProcessor(config);
        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">VehicleModel</param>
        [Route("api/Vehicle/Default/")]
        [HttpPost]
        public void SetDefaultRoles()
        {
            SetDefaultProcessor.SetDefaults();
        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">VehicleModel</param>
        [Route("api/Vehicle/trackRecordStart/")]
        [HttpPost]
        public void recordTrack()
        {
            var track = new TrackModel() { };
            TrackProcessor.RegisterTrack(track, "");
        }

    }
}
