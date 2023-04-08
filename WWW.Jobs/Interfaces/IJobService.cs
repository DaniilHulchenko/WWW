using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Jobs
{
    public interface IJobService
    {
        void Schedule<IJob>(string cron) where IJob : IBackgroundJob;
    }
}
