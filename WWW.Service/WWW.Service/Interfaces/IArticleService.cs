
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;

namespace WWW.Service.Interfaces
{
    public interface IArticleService
    {

        public Task<BaseResponse<IEnumerable<Article>>> GetAll();
        public Task<BaseResponse<IEnumerable<Article>>> GetByCategoryName(string CatName);
    }
}
