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
    public class UserService : Interfaces.IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly EntityBaseRepository<User_Details> _userDetails;

        //private readonly ILogger<IAccountService> _logger;

        public UserService(IUserRepository accountRepository, EntityBaseRepository<User_Details> userDetails, IArticleRepository articleRepository)
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


        public async Task<bool> AddOrDeleteFavoriteEvent(int userId, int articleid)
        {
            var user= await _userRepository.GetValueByID(userId);
            var article = await _articleRepository.GetValueByID(articleid);
            BaseResponse<bool> b;
            if (user.FavEvent.Contains(article))
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
