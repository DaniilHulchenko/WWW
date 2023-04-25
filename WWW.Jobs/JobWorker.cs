using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WWW.API;
using WWW.Jobs.Workers;

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
            //_jobService.Schedule<ArticleApiJob>("* * * * *");
            return Task.CompletedTask;
        }
    }
}