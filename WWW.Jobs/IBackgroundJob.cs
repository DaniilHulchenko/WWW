using Microsoft.Extensions.Hosting;

namespace WWW.Jobs
{
    public interface IBackgroundJob: IHostedService
    {
        //public Task ExecuteAsync(Dictionary<string, string> queryParams = null);
    }
}