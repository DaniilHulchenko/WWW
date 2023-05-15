using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Service.Helpers;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Account;
using WWW.Service.Interfaces;


namespace WWW.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _userRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _userRepository = accountRepository;
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

                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
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
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NickName = model.NickName,
                    Email = model.Email,
                    Introdaction = model.Introdaction,
                    Role = WWW.Domain.Enum.UserRole.User,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };

                await _userRepository.Create(user);
                

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
                //new Claim("Email", user.Email),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }


    }
}
