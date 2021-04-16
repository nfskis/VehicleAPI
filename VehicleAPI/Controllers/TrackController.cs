using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleAPI.BusinessLogic;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;
using VehicleAPI.ViewModels.Tracks;

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
        [Route("api/track")]
        public async Task<ActionResult> RegisterTrackAsync([FromForm] RegisterTrackViewModel value)
        {
            // current user allows to add tracking record.
            // doens't allows to add tracking record for other vehicle.
            string userId = User.Claims.First().Value;
            bool IsUserVehicle = TrackProcessor.IsUserVehicle(userId, value.VehicleSeqID);
            if (User.IsInRole("Admin") || IsUserVehicle)
            {
                _ = await TrackProcessor.RegisterTrackAsync(value);
            }
            else
            {
                return BadRequest("Logined user is not Admin or vehicle user.");
            }

            return Ok("Track data has been registed");
        }

        [HttpGet]
        [Route("api/track")]
        [Authorize(Roles = "Admin")]
        public List<VehicleTrackViewModel> GetTrack(int pageNumber)
        {
            int rowsOfPage = 10;
            return TrackProcessor.GetTracks(pageNumber, rowsOfPage);
        }

        [HttpPut]
        [Route("api/track")]
        [Authorize(Roles = "Admin")]
        public ActionResult PutTrack([FromForm] UpdateTrackViewModel trackModel)
        {
            var result = TrackProcessor.UpdateTrack(trackModel);
            if (result >= 0)
                return Ok($"Updated Track: {trackModel.TrackSeqID}");
            else
                return NotFound($"Failed Updated Track: {trackModel.TrackSeqID}");
        }

        [HttpDelete]
        [Route("api/track")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTrack(string trackSeqID)
        {
            var result = TrackProcessor.DeleteTrack(trackSeqID);
            if (result >= 0)
                return Ok($"Deleted Track successful: {trackSeqID}");
            else
                return NotFound($"Failed delete Track: {trackSeqID}");
        }

        /// <summary>
        /// return Retrieve the positions of a vehicle during a certain time.
        /// </summary>
        /// <param name="id">Vehicle Seq ID</param>
        /// <param name="startTime">2014-04-14 00:00:00.000 or 2021-04-13 T00:00:00.000</param>
        /// <param name="endTime">2014-04-14 00:00:00.000 or 2021-04-13 T00:00:00.000</param>
        /// <returns>JSON</returns>
        [HttpGet]
        [Route("api/track/range")]
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
        [Route("api/track/current")]
        [Authorize(Roles = "Admin")]
        public VehicleTrackViewModel GetCurrentLocationOfVehicle([FromHeader] string VehicleSeqID)
        {
            return TrackProcessor.GetCurrentLocationByVehicleSeqID(VehicleSeqID);
        }
    }
}