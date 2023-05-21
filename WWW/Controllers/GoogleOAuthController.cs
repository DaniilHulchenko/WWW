using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using OAuthTutorial.Helpers;
using System.Security.Claims;
using WWW.Service.Implementations;

namespace WWW.Controllers
{
    public class GoogleOAuthController : Controller
    {
        private readonly GoogleOAuthService _authService;
        private readonly GoogleSingInService _singInFromGoogle;
        private string RedirectUrl = "http://localhost:5188/GoogleOAuth/Code";
        private string[] Scopes;



        private const string PkceSessionKey = "codeVerifier";

        public GoogleOAuthController(GoogleOAuthService authService, GoogleSingInService singInFromGoogle, IConfiguration configuration)
        {
            _authService = authService;
            _singInFromGoogle = singInFromGoogle;
            Scopes = configuration.GetSection("Authentication:Google:Scopes").Get<string[]>();
        }
        //public IActionResult Login(string returnUrl = "/")
        //{
        //    return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, GoogleDefaults.AuthenticationScheme);
        //}

        public IActionResult RedirectOnOAuthServer()
        {
            // PCKE.
            var codeVerifier = Guid.NewGuid().ToString();
            var codeChellange = Sha256Helper.ComputeHash(codeVerifier);

            HttpContext.Session.SetString(PkceSessionKey, codeVerifier);

            var url = _authService.GenerateOAuthRequestUrl(Scopes, RedirectUrl, codeChellange);
            return Redirect(url);
        }

        public async Task<IActionResult> Code(string code)
        {
            string codeVerifier = HttpContext.Session.GetString(PkceSessionKey);
            var tokenResult = await _authService.ExchangeCodeOnTokenAsync(code:code, codeVerifier: codeVerifier ,redirectUrl:RedirectUrl);
            //var refreshedTokenResult = await GoogleOAuthService.RefreshTokenAsync(tokenResult.RefreshToken);


            /*################ Login ###########################*/
                var response = await _singInFromGoogle.RegisterOrLogin(tokenResult.AccessToken);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
            /*###########################################*/

            return BadRequest();
        }



        //[HttpGet]
        //public async Task<ActionResult> GoogleLoginCallback(string returnUrl, string code)
        //{
        //    try
        //    {
        //        var authentication = await Request.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //        if (authentication.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }

        //        var result = await GoogleAuthenticationHelper.ValidateGoogleTokenAsync(Configuration, code, Url.Action("GoogleLoginCallback", "Account", new { returnUrl = returnUrl }));
        //        if (result != null)
        //        {
        //            // Получаем информацию о пользователе из токена
        //            var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, result.UserId),
        //        new Claim(ClaimTypes.Name, result.Name),
        //        new Claim(ClaimTypes.Email, result.Email)
        //    };

        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);

        //            // Аутентифицируем пользователя на сайте и сохраняем информацию о нем в Cookies
        //            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //            return LocalRedirect(returnUrl);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обрабатываем ошибку аутентификации
        //        return RedirectToAction("Login", "Account", new { returnUrl = returnUrl, error = ex.Message });
        //    }

        //    // Обрабатываем ошибку аутентификации
        //    return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
        //}


    }
}
