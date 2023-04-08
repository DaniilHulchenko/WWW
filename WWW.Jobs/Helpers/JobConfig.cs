using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Jobs.Helpers
{
    public class JobConfig
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string Cron { get; set; }
    }
}
