using System;

namespace VehicleAPI.Models
{
    /// <summary>
    /// View model of Tracking 
    /// </summary>
    public class VehicleModel
    {
        public string VehicleSeqID { get; set; } = "";
        public string UserSeqID { get; set; } = "";
        public string PlateNumber { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
    }
}