using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WWW.API.Helpers;

namespace WWW.API { 
    public class APIRequest: ApiAccess
    {
        public APIRequest(string ApiName) : base(ApiName) { }

        public async Task<dynamic> GetData(Dictionary<string, string> queryParams)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var urlBuilder = new UriBuilder(baseUrl);
            urlBuilder.Path = endpoint;
            urlBuilder.Query = BuildQueryString(queryParams);

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
                Console.WriteLine("!!! API Error: " + response.StatusCode);
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

