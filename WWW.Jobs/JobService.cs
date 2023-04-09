using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Jobs.Helpers;

namespace WWW.Jobs.Implementations
{
    public class JobService : IJobService
    {
        ILogger<JobService> _logger;
        IServiceProvider _serviceProvider;
        public Dictionary<Type, JobConfig> _jobs;
        public JobService(IServiceProvider serviceProvider, ILogger<JobService> logger)
        {
            _logger = logger;
            _jobs = new();
            _serviceProvider = serviceProvider;
        }

        public void Schedule<IJob>(string cron) where IJob : IBackgroundJob
        {
            var type = typeof(IJob);
            if (_jobs.ContainsKey(type))
            {
                throw new ArgumentException($"Job type: {type} already exsist");
            }
            RecurringJob.AddOrUpdate<JobService>(type.Name, s => this.ExecuteJob(type), cron);

            _jobs.Add(type, new JobConfig() {
                Name= type.Name,
                Type= type,
                Cron= cron
            });
        }

        public async Task ExecuteJob(Type type)
        {
            try
            {
                using(IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var job = scope.ServiceProvider.GetRequiredService(type) as IBackgroundJob;
                    await job.ExecuteAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! : {ex}");
            }
        }

    }
}
