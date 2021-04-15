using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleAPI.Models
{
    public class ApiResultMessage
    {
        public string Message { get; set; }
        public string Status { get; set; }

        public ApiResultMessage()
        {

        }

        public ApiResultMessage(string message, string status)
        {
            Message = message;
            Status = status;
        }
    }
}
