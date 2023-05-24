using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using WWW.API.Helpers;

namespace WWW.API
{
    public interface IApiRepository<T> 
    {
        public Task<dynamic> GetDataAsync( Dictionary<string, string> queryParams = null);
        public void ApiSelector(string ApiName);

    }
}
