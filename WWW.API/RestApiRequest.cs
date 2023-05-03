using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace WWW.API
{
    public class RestApiRequest
    {
        private readonly IConfiguration _configuration;

        private string _token;
        private string _baseUrl;
        private string _endpoint;


        public RestApiRequest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ApiSelector(string ApiName)
        {
            _token = _configuration[$"API:{ApiName}:token"];
            _baseUrl = _configuration[$"API:{ApiName}:baseUrl"];
            _endpoint = _configuration[$"API:{ApiName}:endpoint"];
        }
        public Task<dynamic> GetDataAsync (Dictionary<string, string> queryParams = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest(_endpoint, Method.Get);
            request.AddParameter("apikey", _token);

            if (queryParams != null)
            {
                foreach (var kvp in queryParams)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                }
            }

            var response = client.Get(request);

            string responseString = response.Content;

            dynamic data = JsonConvert.DeserializeObject<dynamic>(responseString);
            return data;
        }
        public async Task<T> GetDataAsync<T>(Dictionary<string, string> queryParams = null)
        {
            T data = (await GetDataAsync(queryParams)).ToObject<T>();
            return data;
        }
    }
}
