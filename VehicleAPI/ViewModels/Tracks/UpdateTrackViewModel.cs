using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleAPI.ViewModels.Tracks
{
    public class UpdateTrackViewModel
    {
        [Required(ErrorMessage = "TrackSeqID is required")]
        public string TrackSeqID { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }
    }
}
