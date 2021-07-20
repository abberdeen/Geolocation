using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Geolocation.Common
{ 
    public class Location
    {

        [Key]
        public int GeonameId { get; set; }
         
        public string LocaleCode { get; set; }
         
        public string ContinentCode { get; set; }
         
        public string ContinentName { get; set; }
         
        public string CountryCode { get; set; }
         
        public string CountryName { get; set; }
         
        public string Subdivision1IsoCode { get; set; }
         
        public string District { get; set; }
         
        public string Subdivision2IsoCode { get; set; }
         
        public string Region { get; set; }
         
        public string CityName { get; set; }
         
        public string MetroCode { get; set; }
         
        public string TimeZone { get; set; }
         
        public bool IsInEuropeanUnion { get; set; } 
    }
}
