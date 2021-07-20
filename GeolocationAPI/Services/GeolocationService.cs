using Geolocation.API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Geolocation.API.Services
{
    public class GeolocationService : IGeolocationService
    {
        private GeolocationContext _context;

        public GeolocationService(GeolocationContext context) 
        {
            _context = context;
        }
        
        public IpGeolocation FindIpGeolocation(IPAddress ip)
        {
            if (ip == null) 
                return null; 

            var geolocation = _context.Blocks.Where(b => EF.Functions.Contains(b.Network, ip)).FirstOrDefault();
            if (geolocation == null)
                return null;

            var address = _context.Locations.Find(geolocation.GeonameId);
            if (address == null)
                return null;

            var ipGeolocation = new IpGeolocation() {
                CountryName = address.CountryName,
                District = address.District,
                Region = address.Region,
                CityName = address.CityName,
                TimeZone = address.TimeZone,
                IsAnonymousProxy = geolocation.IsAnonymousProxy,
                IsSatelliteProvider = geolocation.IsSatelliteProvider,
                Latitude = geolocation.Latitude, 
                Longitude = geolocation.Longitude,
                AccuracyRadius = geolocation.AccuracyRadius, 
                PostCode = geolocation.PostCode
            };

            return ipGeolocation;
        }
    }
}
