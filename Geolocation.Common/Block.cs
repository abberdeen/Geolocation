using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Common
{ 

    public class Block
    {
        [Key]
        public (IPAddress, int) Network { get; set; }
         
        public int? GeonameId { get; set; }
         
        public int? RegisteredCountryGeonameId { get; set; }
         
        public string RepresentedCountryGeonameId { get; set; }
         
        public int? IsSatelliteProvider { get; set; }
         
        public int? IsAnonymousProxy { get; set; }
         
        public string PostCode { get; set; }
         
        public double Latitude { get; set; }
         
        public double Longitude { get; set; }
         
        public int? AccuracyRadius { get; set; }
    }
}
