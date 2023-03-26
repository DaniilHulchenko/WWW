
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;

namespace WWW.Service.Interfaces
{
    public interface IArticleService: IBaseService<Article>
    {
        public Task<BaseResponse<IEnumerable<Article>>> GetByCategoryName(string CatName);
        //public Task<BaseResponse<IEnumerable<Article>>> foo(int i);
    } 
}
