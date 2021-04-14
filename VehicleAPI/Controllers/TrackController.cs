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
    //[Authorize(Roles = "Admin")]
    public class TrackController : ControllerBase
    {
        private TrackProcessor TrackProcessor;

        public TrackController(IConfiguration config)
        {
            TrackProcessor = new TrackProcessor(config);
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// https://localhost:44309/api/Track/register?VehicleID=dcbdf109-c334-4112-a94b-17bfece248b4
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpPost]
        [Route("api/track/register/")]
        public void Post([FromBody] TrackModel value, string vehicleID)
        {
            TrackProcessor.RegisterTrack(value, vehicleID);
        }


        /// <summary>
        /// return Retrieve the positions of a vehicle during a certain time.
        /// https://localhost:44309/api/track/range/
        /// </summary>
        /// <param name="id">Vehicle Seq ID</param>
        /// <param name="startTime">2014-04-14 00:00:00.000 or 2021-04-13 T00:00:00.000</param>
        /// <param name="endTime">2014-04-14 00:00:00.000 or 2021-04-13 T00:00:00.000</param>
        /// <returns>JSON</returns>
        [HttpPost]
        [Route("api/track/range/")]
        public List<VehicleTrackViewModel> TrackRange(TrackRangeModel trackRange)
        {
            return TrackProcessor.GetTracksByVehicleSeqID(trackRange.Id)
                .Where(curr => curr.CreatedDate >= trackRange.StartTime && curr.CreatedDate <= trackRange.EndTime)
                .ToList();
        }

        /// <summary>
        /// return Retrieve the current position of a vehicle
        /// https://localhost:44309/api/track/current?id=dcbdf109-c334-4112-a94b-17bfece248b4
        /// </summary>
        /// <param name="id">Vehicle Seq ID</param>
        /// <returns>JSON</returns>
        [HttpGet]
        [Route("api/track/current/")]
        public VehicleTrackViewModel GetCurrentLocationOfVehicle(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return TrackProcessor.GetCurrentLocationByVehicleSeqID(id);
            }
            else
            {
                return null;
            }
        }
    }
}