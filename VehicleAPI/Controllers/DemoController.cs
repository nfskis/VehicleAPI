using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VehicleAPI.BusinessLogic;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;

namespace VehicleAPI.Controllers
{
    [ApiController]
    public class DemoController : Controller
    {
        private readonly SetDefaultProcessor SetDefaultProcessor;
        private readonly TrackProcessor TrackProcessor;
        private readonly SqlDataAccess SqlDataAccess;

        public DemoController(IConfiguration config)
        {
            SetDefaultProcessor = new SetDefaultProcessor(config);
            TrackProcessor = new TrackProcessor(config);
            SqlDataAccess = new SqlDataAccess(config);

        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">VehicleModel</param>
        [Route("api/Vehicle/SetDefault/")]
        [HttpPost]
        public void SetDefaultRoles()
        {
            SetDefaultProcessor.SetDefaults();
        }

        /// <summary>
        /// Register Vehicle
        /// </summary>
        /// <param name="value">VehicleModel</param>
        [Route("api/Vehicle/TrackRecord/")]
        [HttpPost]
        public async void recordTrack()
        {
            string query = "SELECT * FROM Vehicles";
            int count = 0;
            while (true)
            {
                Debug.WriteLine($@"Record{count} ON: {DateTime.Now}");
                var vehicles = SqlDataAccess.LoadData<VehicleModel>(query);
                Random rand = new Random();
                for (int i = 0; i < vehicles.Count; i++)
                {
                    var vehicle = vehicles[i];
                    double la = 13.706555 + rand.NextDouble();
                    double lo = 100.597545 + rand.NextDouble();
                    var track = new RegisterTrackViewModel()
                    {
                        VehicleSeqID = vehicle.VehicleSeqID,
                        Latitude = Math.Round(la, 6),
                        Longitude = Math.Round(lo, 6)
                    };

                    TrackProcessor.RegisterTrack(track);
                }
                Debug.WriteLine($@"Record{count} OFF: {DateTime.Now}");                
                await Task.Delay(30000);
                count++;
            }


        }

    }
}
