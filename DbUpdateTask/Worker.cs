using DbUpdateTask.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
using Geolocation.Common;

namespace DbUpdateTask
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                Assembly asm = Assembly.GetExecutingAssembly();
                string path = System.IO.Path.GetDirectoryName(asm.Location);

                var workPath = path + "\\Data\\tmp";

                if (Directory.Exists(workPath))
                    Directory.Delete(workPath, true);

                Directory.CreateDirectory(workPath);

                var geoService = new GeoLiteCitiesService(workPath);

                Console.WriteLine("Начата загрузка файла с maxmind.com");
                var zipFile = geoService.Download();

                Console.WriteLine("Начата разархивация загруженного файла");
                geoService.Unzip(zipFile);

                Console.WriteLine("Начата сохранение данных в базу данных");

                Console.WriteLine("Started saving Locations");

                using (var context = new GeolocationContext())
                {
                    List<Location> locations = (List<Location>)geoService.ParseLocations();

                    context.Database.ExecuteSqlRaw("Delete from \"Locations\"");
                    context.Locations.AddRange(locations);
                    context.SaveChanges();

                    locations = null;
                }

                GC.Collect();

                Console.WriteLine("Locations saving completed");

                Console.WriteLine("Blocks saving started");

                string csvPath = Directory.EnumerateDirectories(workPath).First();

                var blocksFile = csvPath + "\\GeoLite2-City-Blocks-IPv4.csv";
                if (!File.Exists(blocksFile))
                {
                    throw new Exception("Файлы db не загружены или не разархивированы");
                }

                using (var context = new GeolocationContext())
                {
                    context.Database.ExecuteSqlRaw("Delete from \"Blocks\"");

                    List<Block> blocks = new List<Block>();

                    int c = 0;
                    int step = 100000;

                    foreach (var item in BlocksService.ImportBlocksCSV(blocksFile))
                    {
                        blocks.Add(item);

                        if (blocks.Count == step) 
                        {                           
                            context.Blocks.AddRange(blocks);
                            context.SaveChanges();
                            blocks = new List<Block>();
                            GC.Collect();
                            c += step;
                            Console.WriteLine("Added {0} data to Blocks", c);
                        }
                    }
                    c += blocks.Count;
                    Console.WriteLine("Added {0} data to Blocks", c);

                    context.Blocks.AddRange(blocks);
                    context.SaveChanges();
                    blocks = new List<Block>();
                    GC.Collect();
                }
                Console.WriteLine("Blocks saving completed");
                 
                await Task.Delay(10000000, stoppingToken);
            }
        }
    }
}
