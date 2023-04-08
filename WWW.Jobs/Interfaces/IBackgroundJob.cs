using Microsoft.Extensions.Hosting;

namespace WWW.Jobs
{
    public interface IBackgroundJob
    {
        public Task ExecuteAsync();
    }
}