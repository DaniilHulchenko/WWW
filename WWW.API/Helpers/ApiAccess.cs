using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WWW.API.Helpers
{
    public class ApiAccess
    {
        private readonly static string _configPath = "appsettings.json";
        private readonly static string _json = File.ReadAllText(_configPath);
        private readonly dynamic _config;

        public ApiAccess(string apiName)
        {
            //_config = JsonConvert.DeserializeObject<dynamic>(_json).API.Events;
            JObject obj = JObject.Parse(_json);
            _config = obj["API"][apiName].ToObject<dynamic>();

            token = _config.token;
            baseUrl = _config.baseUrl;
            endpoint = _config.endpoint;

        }

        protected string token;
        protected string baseUrl;
        protected string endpoint;


    }
}
