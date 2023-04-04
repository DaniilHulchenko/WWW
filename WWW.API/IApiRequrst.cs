using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.API.Helpers;

namespace WWW.API
{
    public interface IApiRequrst
    {
        public Task<dynamic> GetData(string ApiName,  Dictionary<string, string> queryParams);
        private static string BuildQueryString( Dictionary<string, string> queryParams);

    }
}
