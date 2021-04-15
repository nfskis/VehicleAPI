using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using VehicleAPI.BusinessLogic;
using VehicleAPI.ViewModels;

namespace VehicleAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class TrackController : ControllerBase
    {
        private TrackProcessor TrackProcessor;

        public TrackController(IConfiguration config)
        {
            TrackProcessor = new TrackProcessor(config);
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpPost]
        [Route("api/track/register/")]
        public void RegisterTrack([FromForm] RegisterTrackViewModel value)
        {
            // current user allows to add tracking record.
            // doens't allows to add tracking record for other vehicle.
            bool IsUserVehicle = TrackProcessor.IsUserVehicle(User.Claims.First().Value,
                                                                value.VehicleSeqID);
            if (User.IsInRole("Admin") || IsUserVehicle)
            {
                TrackProcessor.RegisterTrackAsync(value);
            }

        }

        /// <summary>
        /// return Retrieve the positions of a vehicle during a certain time.
        /// </summary>
        /// <param name="id">Vehicle Seq ID</param>
        /// <param name="startTime">2014-04-14 00:00:00.000 or 2021-04-13 T00:00:00.000</param>
        /// <param name="endTime">2014-04-14 00:00:00.000 or 2021-04-13 T00:00:00.000</param>
        /// <returns>JSON</returns>
        [HttpGet]
        [Route("api/track/range/")]
        [Authorize(Roles = "Admin")]
        public List<VehicleTrackViewModel> TrackRange([FromHeader] string VehicleSeqID, DateTime startTime, DateTime endTime)
        {
            return TrackProcessor.GetTracksByVehicleSeqID(VehicleSeqID)
               .Where(curr => DateTime.Compare(curr.CreatedDate, startTime) >= 0
                            && DateTime.Compare(curr.CreatedDate, endTime) <= 0)
               .ToList();
        }

        /// <summary>
        /// return Retrieve the current position of a vehicle
        /// </summary>
        /// <param name="id">Vehicle Seq ID</param>
        /// <returns>JSON</returns>
        [HttpGet]
        [Route("api/track/current/")]
        [Authorize(Roles = "Admin")]
        public VehicleTrackViewModel GetCurrentLocationOfVehicle([FromHeader] string VehicleSeqID)
        {
            return TrackProcessor.GetCurrentLocationByVehicleSeqID(VehicleSeqID);
        }
    }
}