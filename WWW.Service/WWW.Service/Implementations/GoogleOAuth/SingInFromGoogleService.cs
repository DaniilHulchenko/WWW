using System.Data;
using System.Security.Claims;
using WWW.Domain.Entity;
using WWW.Domain.Models.GoogleOAuth;
using WWW.Domain.Response;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

namespace WWW.Service.Implementations
{
    public class SingInFromGoogleService
    {
        private readonly IAccountService _accountService;
        private readonly DownloadService _downloadService;
        public SingInFromGoogleService(IAccountService accountService, DownloadService downloadService)
        {
            _accountService = accountService;
            _downloadService = downloadService;
        }
        public async Task<BaseResponse<ClaimsIdentity>> RegisterOrLogin(string accessToken)
        {
            GoogleUserInfo data = await GoogleApiService.GetUserInfoAsync(accessToken);
            User user = new()
            {
                FirstName = data.name,
                LastName = data.family_name,
                NickName=data.given_name,
                Email = data.email,
                Avatar= await _downloadService.DownloadJpgAsync(data.picture),
                Role = Domain.Enum.UserRole.User,

            };
            var response = _accountService.Authenticate(user);



            return new BaseResponse<ClaimsIdentity>() { Data = response, StatusCode=WWW.Domain.Enum.StatusCode.OK };
        }


    }



}
