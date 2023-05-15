using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using OAuthTutorial.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.GoogleOAuth;

namespace WWW.Service.Implementations
{
    public class GoogleOAuthService
    {
        private readonly string ClientId;
        private readonly string ClientSecret;
        private readonly string OAuthServerEndpoint;
        private readonly string TokenServerEndpoint;

        public GoogleOAuthService(IConfiguration configuration)
        {
            ClientId = configuration["Authentication:Google:ClientId"];
            ClientSecret = configuration["Authentication:Google:ClientSecret"];
            OAuthServerEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
            TokenServerEndpoint = "https://oauth2.googleapis.com/token";
        }


        public string GenerateOAuthRequestUrl(string[] scopes, string redirectUrl, string codeChellange)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"client_id", ClientId},
                { "redirect_uri", redirectUrl },
                { "response_type", "code" },
                { "scope", string.Join(" ", scopes) },
                { "code_challenge", codeChellange },
                { "code_challenge_method", "S256" },
                { "access_type", "offline" }
            };

            var url = QueryHelpers.AddQueryString(OAuthServerEndpoint, queryParams);
            return url;
        }

        public async Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier, string redirectUrl)
        {
            var authParams = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "code", code },
                { "code_verifier", codeVerifier },
                { "grant_type", "authorization_code" },
                { "redirect_uri", redirectUrl }
            };


            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(TokenServerEndpoint, authParams);
            return tokenResult;
        }

        public async Task<TokenResult> RefreshTokenAsync(string refreshToken)
        {
            var refreshParams = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(TokenServerEndpoint, refreshParams);

            return tokenResult;
        }
    }
}
