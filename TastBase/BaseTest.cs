using NuGet.Common;

namespace TastBase
{
    public class BaseTest
    {
        public IServiceProvider serviceProvider { get; private set; }
        public ILogger logger { get; private set; }
        protected BaseTest() { 
            
        }

    }
}