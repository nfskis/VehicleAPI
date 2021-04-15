using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using VehicleAPI.Models;

namespace VehicleAPI.ViewModels
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
        public string GoogleMapAddress
        {
            get
            {
                string result = string.Empty;
                string googleApiKey = @"AIzaSyASMSCzALUYRux5jOgqM9A7T1zNeK80e0w";
                string url = $@"https://maps.googleapis.com/maps/api/geocode/json?latlng={Latitude},{Longitude}&key={googleApiKey}";

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader stream = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                result = stream.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                var geoCodingModel = JsonSerializer.Deserialize<GeoCodingModel>(result);
                if(geoCodingModel.status == "OK" && geoCodingModel.results.Any())
                {
                    return geoCodingModel.results.First().formatted_address;
                }

                return "NOT FOUND ADDRESS";
            }
        }
        public DateTime CreatedDate { get; set; }
    }



}