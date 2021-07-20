using Geolocation.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DbUpdateTask.Services
{
    public class GeoLiteCitiesService
    {
        private string _workPath;
        public GeoLiteCitiesService(string workPath)
        {
            _workPath = workPath; 
        }

        public IEnumerable<Block> ParseBlocks()
        {
            string csvPath = Directory.EnumerateDirectories(_workPath).First();

            var blocksFile = csvPath + "\\GeoLite2-City-Blocks-IPv4.csv";
            if (File.Exists(blocksFile)) 
            {
                return BlocksService.ImportBlocksCSV(blocksFile);
            }
            throw new Exception("Файлы db не загружены или не разархивированы");
        }

        public IEnumerable<Location> ParseLocations()
        {
            string csvPath = Directory.EnumerateDirectories(_workPath).First();

            var locationsFile = csvPath + "\\GeoLite2-City-Locations-ru.csv";
            if (File.Exists(locationsFile))
            {
                return LocationsService.ImportLocationsCSV(locationsFile);
            }
            throw new Exception("Файлы db не загружены или не разархивированы");
        }

        public void Unzip(string zipFile) 
        {
            ZipFile.ExtractToDirectory(zipFile, _workPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Downloaded file path</returns>
        public string Download()
        {
            string downlaodedFile = _workPath + "\\geodb.zip"; 

            using (var wc = new WebClient())
            {
                wc.UseDefaultCredentials = true;
                wc.DownloadProgressChanged += HandleDownloadProgress;
                wc.DownloadFileCompleted += HandleDownloadComplete;

                var syncObject = new Object();
                lock (syncObject)
                {
                    wc.DownloadFileAsync(EndpointUrl, downlaodedFile, syncObject);
                    //This would block the thread until download completes
                    Monitor.Wait(syncObject);
                }
            } 

            return downlaodedFile;
        }

        public void HandleDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            lock (e.UserState)
            {
                //releases blocked thread
                Monitor.Pulse(e.UserState);
            }
        }


        public void HandleDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("    downloaded {0} of {1} bytes. {2} % complete...",
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
        }

        public Uri EndpointUrl
        {
            get
            {
                var licenceKey = "Noj1fefmmFLAaXfN";
                var editionId = "GeoLite2-City-CSV";
                var endpoint = "https://download.maxmind.com/app/geoip_download?edition_id=" + editionId + "&license_key=" + licenceKey + "&suffix=zip";
                return new Uri(endpoint);
            }
        }
    }
}
