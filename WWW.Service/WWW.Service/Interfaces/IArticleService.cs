
using WWW.Domain.Response;

namespace WWW.Service.Interfaces
{
    public interface IArticleService
    {

        public Task<IBaseResponse<IEnumerable<Domain.Entity.Article>>> GetAll();
    }
}
