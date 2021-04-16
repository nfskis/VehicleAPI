using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;
using VehicleAPI.ViewModels.Tracks;

namespace VehicleAPI.BusinessLogic
{
    public class TrackProcessor
    {
        private readonly SqlDataAccess SqlDataAccess;
        private readonly VehicleProcessor VehicleProcessor;

        public TrackProcessor(IConfiguration config)
        {
            SqlDataAccess = new SqlDataAccess(config);
            VehicleProcessor = new VehicleProcessor(config);
        }

        /// <summary>
        /// return all tracks of vehicle
        /// </summary>
        /// <param name="vehicleSeqID"></param>
        /// <returns></returns>
        public List<VehicleTrackViewModel> GetTracksByVehicleSeqID(string vehicleSeqID)
        {
            return SqlDataAccess.LoadData<VehicleTrackViewModel, dynamic>("dbo.Track_TrackAllByVehicleSeqID", new { vehicleSeqID });
        }

        /// <summary>
        /// return Current Vehicle location 
        /// </summary>
        /// <param name="vehicleSeqID">Target vehicle sequense ID</param>
        /// <returns></returns>
        public VehicleTrackViewModel GetCurrentLocationByVehicleSeqID(string vehicleSeqID)
        {
            return SqlDataAccess.LoadSingleData<VehicleTrackViewModel, dynamic>("dbo.Track_GetCurrentLocation", new { vehicleSeqID });
        }

        /// <summary>
        /// return update row count
        /// </summary>
        /// <param name="track">track model item</param>
        /// <returns></returns>
        public int UpdateTrack(UpdateTrackViewModel track)
        {
            return SqlDataAccess.StoredProcesdure("dbo.Track_UpdateTrack", track);
        }

        /// <summary>
        /// get all tracks
        /// </summary>
        /// <returns></returns>
        internal List<VehicleTrackViewModel> GetTracks()
        {
            return SqlDataAccess.LoadData<VehicleTrackViewModel>("dbo.Track_GetAllTracks");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public bool IsUserVehicle(string userID, string vehicleID)
        {
            return VehicleProcessor.FindVehicleByVehicleID(vehicleID).UserSeqID == userID;
        }

        /// <summary>
        /// delete track
        /// </summary>
        /// <param name="trackSeqID"></param>
        internal int DeleteTrack(string trackSeqID)
        {
            return SqlDataAccess.SaveData("dbo.track_delete", new { trackSeqID });
        }

        /// <summary>
        /// return Current Vehicle location 
        /// </summary>
        /// <param name="vehicleSeqID">Target vehicle sequense ID</param>
        /// <returns></returns>
        public async Task<int> RegisterTrackAsync(RegisterTrackViewModel value)
        {
            // Check to eixted Vehicle ID
            bool isExisted = VehicleProcessor.FindVehicleByVehicleID(value.VehicleSeqID) != null ? true : false;
            if (!isExisted)
            {
                throw new Exception($"the givens VehicleID is not existed in DataBase: '{value.VehicleSeqID}'");
            }

            #region Compare current location and previous position
            // compare current position and previous position.            
            var current = GetCurrentLocationByVehicleSeqID(value.VehicleSeqID);
            if (current != null)
            {
                if (Math.Abs(current.Longitude - value.Longitude) <= 0.000001
                    && Math.Abs(current.Latitude - value.Latitude) <= 0.000001)
                {
                    // Vehicle stpped at somewhere in 30sec. so, It waste keep recored location. 
                    // So, update current record renewal for time 
                    return UpdateTrack(new UpdateTrackViewModel()
                    {
                        TrackSeqID = current.TrackSeqID,
                        Latitude = current.Latitude,
                        Longitude = current.Longitude,
                    }); //return -1;
                }
            }
            #endregion

            return await SqlDataAccess.SaveDataAsync<TrackModel, dynamic>("dbo.Track_RegisterTrack",
                                                                new
                                                                {
                                                                    TrackSeqID = Guid.NewGuid().ToString(),
                                                                    VehicleSeqID = value.VehicleSeqID,
                                                                    Latitude = value.Latitude,
                                                                    Longitude = value.Longitude
                                                                });
        }

    }
}