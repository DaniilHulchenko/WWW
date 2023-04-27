
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Article;

namespace WWW.Service.Interfaces
{
    public interface IArticleService:  IBaseService<Article>
    {
        Task<BaseResponse<IEnumerable<Article>>> GetByCategoryName(string CatName);
        Task<BaseResponse<Article>> GetById(int id);
        Task<bool> AddTag(Article article,Tags tags);
        Task<bool> Create(ArticleCreateViewModal entity);

    }
}
