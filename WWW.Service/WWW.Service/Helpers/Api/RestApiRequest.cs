using RestSharp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using WWW.Service.Helpers.Api;
using WWW.Domain.Api;
//using WWW.Domain.Api;

namespace WWW.Service.Helpers
{
    public class RestApiRequest: IRestApiRequest
    {
        private readonly IConfiguration _configuration;

        private string _token;
        private string _baseUrl;
        private string _endpoint;
        //private readonly Logger<RestApiRequest> _logger;

        public RestApiRequest()
        {
        }

        public RestApiRequest(IConfiguration configuration)
        {
            _configuration = configuration;
            //_logger = logger;
        }

        public void ApiSelector(string ApiName)
        {
                _token = _configuration[$"API:{ApiName}:token"];
                _baseUrl = _configuration[$"API:{ApiName}:baseUrl"];
                _endpoint = _configuration[$"API:{ApiName}:endpoint"];
        }
        public async Task<dynamic> GetDataAsync (Dictionary<string, string> queryParams = null)
        {
            if (_token == null) { throw new ArgumentException("Api didn't find, cheap Api Selector arguments"); }
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("!!!!!" + ex.Message);
            //    //throw new Exception(ex.Message);
            //}
            
        }
        public async Task<T> GetDataAsync<T>(Dictionary<string, string> queryParams = null) // where T: BaseApiModel
        {
            T data = (await GetDataAsync(queryParams)).ToObject<T>();
            return data;
        }
    }
}
