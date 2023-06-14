
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Domain.Enum.Articles;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Article;

namespace WWW.Service.Interfaces
{
    public interface IArticleService:  IBaseService<Article>
    {
        Task<BaseResponse<IQueryable<Article>>> GetByCity( string City);
        Task<IQueryable<Article>> SearchByTitle(IQueryable<Article> articles, string searchTerm);
        Task<BaseResponse<IQueryable<Article>>> GetByCategoryName(string CatName);
        Task<BaseResponse<IQueryable<Article>>> GetByCategoryNameFilter(IQueryable<Article>? articles, string CatName);

        BaseResponse<Article> GetBySlug(string slug);
        Task<BaseResponse<Article>> GetById(int id);
        Task<bool> AddTag(Article article,Tags tags);
        Task<bool> Create(ArticleCreateViewModal entity);
        Task<BaseResponse<IQueryable<Article>>> OrderBy(IQueryable<Article> articles, ArticleSortOption SortOption);
        Task<BaseResponse<IQueryable<Article>>> Filter(IQueryable<Article> articles, Dictionary<string,string> Filters);
    }
}
