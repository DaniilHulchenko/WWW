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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _userRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly EntityBaseRepository<User_Details> _userDetails;

        //private readonly ILogger<IAccountService> _logger;

        public AccountService(IAccountRepository accountRepository, EntityBaseRepository<User_Details> userDetails, IArticleRepository articleRepository)
        {
            //_logger = logger;
            _userRepository = accountRepository;
            _userDetails = userDetails;
            _articleRepository = articleRepository;
        }
        public Task<bool> Create(User category)
        {
            throw new Exception();
        }

        public Task<bool> Delete(int i)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<User>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<User>> GetByName(string name)
        {
            var user = await _userRepository.GetALL().FirstOrDefaultAsync(u=>u.NickName==name);
            if (user != null)
            {
                return new BaseResponse<User>() { 
                    Data = user,
                    StatusCode=StatusCode.OK
                };
            }
            return new BaseResponse<User>()
            {
                StatusCode = StatusCode.NotFound
            };
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
                    //FirstName = model.FirstName,
                    //LastName = model.LastName,
                    NickName = model.NickName,
                    Email = model.Email,
                    //Introdaction = model.Introdaction,
                    Role = WWW.Domain.Enum.UserRole.User,
                    //Avatar = model.Avatar,
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

        public async Task<bool> AddOrDeleteFavoriteEvent(int userId, int articleid)
        {
            var user= await _userRepository.GetValueByID(userId);
            var article = await _articleRepository.GetValueByID(articleid);
            BaseResponse<bool> b;
            if (user.Event.Contains(article))
            {
                b = _userRepository.DeleteEventFromFavorite(user, article);
            }
            else
            {
                b = _userRepository.AddEventToFavorite(user, article);
            }

            if (b.Data == false)
            {
                //_logger.LogError("!!!!! "+b.ErrorDescription);
            }
            return b.Data;
        }
    }
}
