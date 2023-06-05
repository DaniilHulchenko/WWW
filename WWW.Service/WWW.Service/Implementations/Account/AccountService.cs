using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Account;
using WWW.Service.Helpers;
using WWW.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;

namespace WWW.Service.Implementations
{
    public class AccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly EntityBaseRepository<User_Details> _userDetails;


        public AccountService(IUserRepository accountRepository, EntityBaseRepository<User_Details> userDetails, IArticleRepository articleRepository)
        {
            _userRepository = accountRepository;
            _userDetails = userDetails;
            _articleRepository = articleRepository;
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetALL().FirstOrDefaultAsync(x => x.NickName == model.NickName);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        ErrorDescription = "User not found"
                    };
                }
                if (user.Details.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        ErrorDescription = "Invalid password or user name "
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!"+ex+ $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    ErrorDescription = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetALL().FirstOrDefaultAsync(x => x.NickName == model.NickName);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        ErrorDescription = "Користувач с таким логином вже є",
                    };
                }

                user = new User()
                {
                    NickName = model.NickName,
                    Email = model.Email,
                    Role = WWW.Domain.Enum.UserRole.User,
                };
                if (model.Avatar != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Avatar.CopyToAsync(memoryStream);
                        user.Avatar = memoryStream.ToArray();
                    }
                }
                await _userRepository.Create(user);


                await _userDetails.Create(new User_Details()
                {
                    User = user,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                    Introdaction = model.Introdaction,
                });


                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!"+ex+ $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    ErrorDescription = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                new Claim("UserId", user.Id.ToString()),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

       









    }
}
