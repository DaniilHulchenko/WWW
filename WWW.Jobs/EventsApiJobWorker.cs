using Hangfire;
using Microsoft.Extensions.Hosting;
using WWW.API;


namespace WWW.Jobs
{
    public class EventsApiJobWorker : BackgroundService
    {
        private readonly IBackgroundApiJob<RestApiRequest> _restApiRequrst;
        //private readonly IJobService _jobService;

        public EventsApiJobWorker(IBackgroundApiJob<RestApiRequest> restApiRequrst)
        {
            //_jobService = jobService;
            _restApiRequrst = restApiRequrst;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _restApiRequrst.ApiSelector("Events:ticketmaster");
            dynamic data = await _restApiRequrst.GetDataAsync();
            
            throw new NotImplementedException();
        }
    }
}