using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocation.API.ViewModel
{
    public class IpGeolocation
    {
        public string CountryName { get; set; }

        public string District { get; set; } 

        public string Region { get; set; }

        public string CityName { get; set; }
          
        public string TimeZone { get; set; } 

        public int? IsSatelliteProvider { get; set; }

        public int? IsAnonymousProxy { get; set; }

        public string PostCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int? AccuracyRadius { get; set; }

    }
}
