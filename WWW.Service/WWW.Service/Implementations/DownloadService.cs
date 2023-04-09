using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Service.Interfaces
{
    public class DownloadService
    {
        ILogger<DownloadService> _logger;   
        public DownloadService(ILogger<DownloadService> logger) { 
            _logger = logger;
        }
        public void DownloadPng(string url)
        {
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "Media", "png", $"{DateTime.Today.ToString("dd.MM.yyyy")}", "test.jpg");

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, fileName);
            }

            _logger.LogInformation($"Pictre was saved in : {fileName}");

        }
    }
}
