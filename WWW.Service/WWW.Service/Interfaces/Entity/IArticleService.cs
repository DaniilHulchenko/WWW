
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Article;

namespace WWW.Service.Interfaces
{
    public interface IArticleService:  IBaseService<Article>
    {
        Task<BaseResponse<IQueryable<Article>>> GetByCategoryName(string CatName);
        Task<BaseResponse<Article>> GetById(int id);
        Task<bool> AddTag(Article article,Tags tags);
        Task<bool> Create(ArticleCreateViewModal entity);
        Task<BaseResponse<IQueryable<Article>>> OrderBy(IQueryable<Article> articles, ArticleSortOption SortOption);
    }
}
