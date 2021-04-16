using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleAPI.ViewModels.Vehicles
{
    public class DeleteVehicleViewModel
    {
        [Required]
        [Display(Name = "type Vehicle PlateNumber")]
        [StringLength(50, ErrorMessage = "maxiumn typing legnth 50")]
        [RegularExpression(@"\d{2}-? *-?\d{4}", ErrorMessage = "Plate Number is required and must be properly formatted")]
        public string PlateNumber { get; set; }
    }
}
