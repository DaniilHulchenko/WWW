using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Account;

namespace WWW.Service.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        //public Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        //public Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        //public ClaimsIdentity Authenticate(User user);


        public Task<BaseResponse<User>> GetByName(string name);

        public Task<bool> Create(User category);

        public Task<bool> Delete(int id);

        public Task<BaseResponse<IEnumerable<User>>> GetAll();

        public Task<bool> AddOrDeleteFavoriteEvent(int userId, int articleid);



    }
}
