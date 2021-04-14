using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using VehicleAPI.DBHelpers;
using VehicleAPI.Models;

namespace VehicleAPI.BusinessLogic
{
    public class TrackProcessor
    {
        private SqlDataAccess SqlDataAccess;

        public TrackProcessor(IConfiguration config)
        {
            SqlDataAccess = new SqlDataAccess(config);
        }

        /// <summary>
        /// return all tracks of vehicle
        /// </summary>
        /// <param name="vehicleSeqID"></param>
        /// <returns></returns>
        public List<VehicleTrackViewModel> GetTracksByVehicleSeqID(string vehicleSeqID)
        {
            string query = $@"SELECT 
                                Vehicles.VehicleSeqID as VehicleSeqID,
                                Vehicles.PlateNumber as PlateNumber,
                                Tracks.Latitude as Latitude,
                                Tracks.Longitude as Longitude,   
                                Tracks.CreatedDate as CreatedDate
                            FROM Vehicles
                            INNER JOIN Tracks ON Tracks.VehicleSeqID = Vehicles.VehicleSeqID
                            WHERE Vehicles.VehicleSeqID = '{vehicleSeqID}'
                            ORDER BY CreatedDate DESC";
            return SqlDataAccess.LoadData<VehicleTrackViewModel>(query);
        }

        /// <summary>
        /// return Current Vehicle location 
        /// </summary>
        /// <param name="vehicleSeqID">Target vehicle sequense ID</param>
        /// <returns></returns>
        public VehicleTrackViewModel GetCurrentLocationByVehicleSeqID(string vehicleSeqID)
        {
            string query = $@"SELECT TOP 1
                                Vehicles.VehicleSeqID as VehicleSeqID,
                                Vehicles.PlateNumber as PlateNumber,
                                Tracks.TrackSeqID as TrackSeqID,
                                Tracks.Latitude as Latitude,
                                Tracks.Longitude as Longitude,   
                                Tracks.CreatedDate as CreatedDate
                            FROM Vehicles
                            INNER JOIN Tracks ON Tracks.VehicleSeqID = Vehicles.VehicleSeqID
                            WHERE Vehicles.VehicleSeqID = '{vehicleSeqID}'
                            ORDER BY Tracks.CreatedDate DESC";
            return SqlDataAccess.SingleOrDefault<VehicleTrackViewModel>(query);
        }

        /// <summary>
        /// return update row count
        /// </summary>
        /// <param name="track">track model item</param>
        /// <returns></returns>
        public int UpdateTrack(TrackModel track)
        {
            string query = $@"UPDATE Tracks
                              SET Latitude = @Latitude, Longitude = @Longitude, CreatedDate = @CreatedDate
                              WHERE TrackSeqID = '{track.TrackSeqID}' ";
            return SqlDataAccess.SaveData(query, track);
        }

        /// <summary>
        /// return Current Vehicle location 
        /// </summary>
        /// <param name="vehicleSeqID">Target vehicle sequense ID</param>
        /// <returns></returns>
        public int RegisterTrack(TrackModel value, string vehicleID)
        {
            // Check Vehicle ID
            string query = $@"SELECT VehicleSeqID 
                              FROM Vehicles 
                              WHERE VehicleSeqID = '{vehicleID}'";
            bool isExisted = SqlDataAccess.SingleOrDefault<VehicleModel>(query) != null ? true : false;
            if (!isExisted)
            {
                throw new Exception($"the givens VehicleID is not existed in DataBase: '{vehicleID}'");
            }
            value.VehicleSeqID = vehicleID;

            #region Compare current location and previous position
            // compare current position and previous position.            
            var current = GetCurrentLocationByVehicleSeqID(vehicleID);
            if (Math.Abs(current.Longitude - value.Longitude) <= 0.000001
                && Math.Abs(current.Latitude - value.Latitude) <= 0.000001)
            {
                // Vehicle stpped at somewhere in 30sec. so, It waste keep recored location. 
                // So, update current record renewal for time 
                value.TrackSeqID = current.TrackSeqID;
                value.CreatedDate = DateTime.Now;
                return UpdateTrack(value); //return -1;
            }
            #endregion

            if (string.IsNullOrWhiteSpace(value.TrackSeqID))
                value.TrackSeqID = Guid.NewGuid().ToString();

            query = @"  INSERT INTO Tracks(TrackSeqID, VehicleSeqID, Latitude, Longitude)
                        VALUES(@TrackSeqID, @VehicleSeqID, @Latitude, @Longitude)";

            return SqlDataAccess.SaveData(query, value);
        }

    }
}