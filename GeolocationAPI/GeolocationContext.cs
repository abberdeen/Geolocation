using Geolocation.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocation.API
{
    public class GeolocationContext : DbContext
    {
        public GeolocationContext(DbContextOptions<GeolocationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Block> Blocks { get; set; }


    }
}
