using CsvHelper; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;
using System.Net;
using CsvHelper.TypeConversion;
using Geolocation.Common;

namespace DbUpdateTask.Services
{
    public class BlocksService
    {
        public static IEnumerable<Block> ImportBlocksCSV(string csvFilePath)
        {  
            using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
            {
                CsvReader csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ",",
                });   
                csvReader.Context.RegisterClassMap<CSVMap>();
                // var blocks = csvReader.GetRecords<Block>().ToList();
                while (csvReader.Read())
                {
                    yield return csvReader.GetRecord<Block>();
                } 
            }
        }
        private sealed class CSVMap : ClassMap<Block>
        {
            public CSVMap()
            {
                Map(m => m.Network).Index(0).TypeConverter<IPNetworkConverter<IPAddress>>();
                Map(m => m.GeonameId).Index(1);
                Map(m => m.RegisteredCountryGeonameId).Index(2);
                Map(m => m.RepresentedCountryGeonameId).Index(3);
                Map(m => m.IsAnonymousProxy).Index(4);
                Map(m => m.IsSatelliteProvider).Index(5);
                Map(m => m.PostCode).Index(6);
                Map(m => m.Latitude).Index(7).TypeConverter<DoubleConverter<double>>();
                Map(m => m.Longitude).Index(8).TypeConverter<DoubleConverter<double>>();
                Map(m => m.AccuracyRadius).Index(9);
            }
        }

        private sealed class IPNetworkConverter<T> : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                var n = text.Split("/");
                return (IPAddress.Parse(n[0]), int.Parse(n[1]));
            }

            public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return value.ToString();
            }
        }

        private sealed class DoubleConverter<T> : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (text.Trim() == "") return (double)0;
                return double.Parse(text.Replace(".", ","));
            }

            public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return value.ToString();
            }
        }
    }
}
