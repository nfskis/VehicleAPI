using System;

namespace VehicleAPI.Models
{
    /// <summary>
    /// View model of Tracking 
    /// </summary>
    public class VehicleTrackViewModel
    {
        public string VehicleSeqID { get; set; }
        public string TrackSeqID { get; set; }
        public string PlateNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}