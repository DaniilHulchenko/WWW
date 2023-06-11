using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWW.Service.Helpers.Api
{
    public interface IRestApiRequest
    {
        void ApiSelector(string ApiName);
        Task<T> GetDataAsync<T>(Dictionary<string, string> parameters);
    }
}
 