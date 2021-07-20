using Geolocation.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUpdateTask
{
    public class GeolocationContext : DbContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=Hybrid;Username=postgres;Password=local;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Block> Blocks { get; set; }
    }

}
