using Geolocation.API.Services;
using Geolocation.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Geolocation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocationController : ControllerBase
    {
        private IGeolocationService _geolocationService;
        public GeolocationController(IGeolocationService geolocationService)
        {
            _geolocationService = geolocationService;
        }

        [HttpGet]
        public ActionResult<IpGeolocation> Get(string ip) 
        {
            IPAddress ipAddress;
            IPAddress.TryParse(ip, out ipAddress);

            if (ipAddress == null)
                return BadRequest();

            var ipGeolocation = _geolocationService.FindIpGeolocation(ipAddress);

            if (ipGeolocation == null)
                return NotFound();

            return ipGeolocation;
        }
    }
}
