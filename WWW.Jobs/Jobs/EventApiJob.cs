using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.API;

namespace WWW.Jobs.Workers
{
    public class EventApiJob : IBackgroundJob
    {
        ILogger<EventApiJob> _logger;
        private readonly RestApiRequest _restapiRepository;
        public EventApiJob(RestApiRequest restapiRepository, ILogger<EventApiJob> logger)
        {
            _logger = logger;
            _restapiRepository = restapiRepository;
        }
        public async Task ExecuteAsync()
        {
            _restapiRepository.ApiSelector("Events:ticketmaster");
            dynamic data = await _restapiRepository.GetDataAsync();
            _logger.LogInformation($"!!! : {data._embedded.events[0].name}");
        }
    }
}
