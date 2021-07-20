using CsvHelper;
using CsvHelper.Configuration;
using Geolocation.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUpdateTask.Services
{
    public class LocationsService
    {
        public static IEnumerable<Location> ImportLocationsCSV(string csvFile)
        { 
            using (var reader = new StreamReader(csvFile, Encoding.UTF8))
            {
                CsvReader csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ",",
                });
                csvReader.Context.RegisterClassMap<CSVMap>();
                var locations = csvReader.GetRecords<Location>().ToList();
                return locations;
            }
        }

        private sealed class CSVMap : ClassMap<Location>
        {
            public CSVMap()
            {
                Map(m => m.GeonameId).Index(0);
                Map(m => m.LocaleCode).Index(1);
                Map(m => m.ContinentCode).Index(2);
                Map(m => m.ContinentName).Index(3);
                Map(m => m.CountryCode).Index(4);
                Map(m => m.CountryName).Index(5);
                Map(m => m.Subdivision1IsoCode).Index(6);
                Map(m => m.District).Index(7);
                Map(m => m.Subdivision2IsoCode).Index(8);
                Map(m => m.Region).Index(9);
                Map(m => m.CityName).Index(10);
                Map(m => m.MetroCode).Index(11);
                Map(m => m.TimeZone).Index(12);
                Map(m => m.IsInEuropeanUnion).Index(13); 
            }
        }
    }
}
