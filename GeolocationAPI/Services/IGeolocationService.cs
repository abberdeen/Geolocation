using Geolocation.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Geolocation.API.Services
{
    public interface IGeolocationService
    {
        public IpGeolocation FindIpGeolocation(IPAddress ip);
    }
}
