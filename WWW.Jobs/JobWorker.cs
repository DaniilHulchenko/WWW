using Microsoft.Extensions.Hosting;
using WWW.Jobs.Jobs;

namespace WWW.Jobs
{
    public class JobWorker : BackgroundService
    {
        private readonly IJobService _jobService;

        public JobWorker(IJobService jobService)
        {
            _jobService = jobService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Jobs Here
            //_jobService.Schedule<EventApiJob_ParseToDb>("* * * * *");
            //_jobService.Schedule<EventApiJob_ParseToDb>("");
            return Task.CompletedTask;
        }
    }
}