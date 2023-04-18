
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Article;

namespace WWW.Service.Interfaces
{
    public interface IArticleService:  IBaseService<Article>
    {
        public Task<BaseResponse<IEnumerable<Article>>> GetByCategoryName(string CatName);
        public Task<BaseResponse<Article>> GetById(int id);
        public Task<bool> AddTag(Article article,Tags tags);
    } 
}
