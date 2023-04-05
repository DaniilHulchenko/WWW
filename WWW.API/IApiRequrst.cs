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
        public Task<dynamic> GetData( Dictionary<string, string> queryParams);
        public void SetApiName(string ApiName);
        //private static string BuildQueryString(Dictionary<string, string> queryParams);

    }
}
