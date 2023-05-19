using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.Models.GoogleOAuth;
using WWW.Domain.Response;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

namespace WWW.Service.Implementations
{
    public class GoogleSingInService
    {
        private readonly IAccountService _accountService;
        private readonly DownloadService _downloadService;
        private readonly IAccountRepository _accountRepository;
        public GoogleSingInService(IAccountService accountService, DownloadService downloadService, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _accountService = accountService;

            _downloadService = downloadService;
        }
        public async Task<BaseResponse<ClaimsIdentity>> RegisterOrLogin(string accessToken)
        {
            GoogleUserInfo data = await GoogleApiParseService.GetUserInfoAsync(accessToken);
            User user = new()
            {
                NickName=data.given_name,
                Email = data.email,
                Avatar= await _downloadService.DownloadJpgAsync(data.picture),
                Role = Domain.Enum.UserRole.User,
            };
            
            if (await _accountRepository.GetALL().FirstOrDefaultAsync(u => u.NickName == user.NickName) == null)
            {
                await _accountRepository.Create(user);
            }

            var response = _accountService.Authenticate(user);



            return new BaseResponse<ClaimsIdentity>() { Data = response, StatusCode=WWW.Domain.Enum.StatusCode.OK };
        }


    }



}
