using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string baseUrl = "https://api.predicthq.com";
        string endpoint = "/v1/events/";
        string token = "JCz8hnygtt2cfk-gWP11CRVCmCP6vsOGENbrHEFx";
        var queryParams = new Dictionary<string, string>
        {
            { "country", "CA" },
        };

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

            Console.WriteLine(data.results[0].title);
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }

    static string BuildQueryString(Dictionary<string, string> queryParams)
    {
        var query = new List<string>();
        foreach (var param in queryParams)
        {
            query.Add($"{param.Key}={param.Value}");
        }
        return string.Join("&", query);
    }
}
