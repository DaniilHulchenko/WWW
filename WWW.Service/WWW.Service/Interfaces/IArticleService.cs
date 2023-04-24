
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Domain.ViewModels.Article;

namespace WWW.Service.Interfaces
{
    public interface IArticleService:  IBaseService<Event>
    {
        public Task<BaseResponse<IEnumerable<Event>>> GetByCategoryName(string CatName);
        public Task<BaseResponse<Event>> GetById(int id);
        public Task<bool> AddTag(Event article,Tags tags);
    } 
}
