using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Entity;
using WWW.Domain.Response;

namespace WWW.DAL.Interfaces
{
    public interface IAccountRepository : IBaseRepository<User>
    {
        public BaseResponse<bool> AddEventToFavorite(User user, Article article);
        public BaseResponse<bool> DeleteEventFromFavorite(User user, Article article);
    }
}
