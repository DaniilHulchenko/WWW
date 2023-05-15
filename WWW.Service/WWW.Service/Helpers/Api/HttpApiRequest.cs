using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
//using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
//using WWW.API.Helpers;

namespace WWW.API {
    public class HttpApiRequest : IApiRepository<HttpApiRequest>
    {
        private string _token;
        private string _baseUrl;
        private string _endpoint;
        private readonly IConfiguration _configuration;

        public HttpApiRequest(IConfiguration configuration) { 
            _configuration = configuration;
        }
        
        public void ApiSelector(string ApiName)
        {
            _token = _configuration[$"API:{ApiName}:token"];
            _baseUrl = _configuration[$"API:{ApiName}:baseUrl"];
            _endpoint= _configuration[$"API:{ApiName}:endpoint"];
        }

        public async Task<dynamic> GetDataAsync(Dictionary<string, string> queryParams = null)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var urlBuilder = new UriBuilder(_baseUrl);
            urlBuilder.Path = _endpoint;
            if (queryParams != null) { 
                urlBuilder.Query = BuildQueryString(queryParams);
                }

            var response = await client.GetAsync(urlBuilder.Uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject<dynamic>(responseString);
                return data;
            }
            else
            {
                return response.StatusCode;
                //Console.WriteLine("!!! API Error: " + response.StatusCode);
            }
        }

        private static string BuildQueryString(Dictionary<string, string> queryParams)
        {
            var query = new List<string>();
            foreach (var param in queryParams)
            {
                query.Add($"{param.Key}={param.Value}");
            }
            return string.Join("&", query);
        }


    }
    }

