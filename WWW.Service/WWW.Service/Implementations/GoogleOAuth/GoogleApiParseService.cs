using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Models.GoogleOAuth;
using WWW.Domain.Response;

namespace WWW.Service.Implementations
{
    public static class GoogleApiParseService
    {
        public static async Task<GoogleUserInfo> GetUserInfoAsync(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.RequestMessage.ToString());
                }

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GoogleUserInfo>(content);
                return data;
            }
        }



        









    }
}
