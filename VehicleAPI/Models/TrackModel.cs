using System;

namespace VehicleAPI.Models
{
    /// <summary>
    /// View model of Tracking 
    /// </summary>
    public class TrackModel
    {
        public string TrackSeqID { get; set; }
        public string VehicleSeqID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}