
using WWW.Domain.Response;

namespace WWW.S.Interfaces
{
    public interface IArticleService
    {

        public Task<BaseResponse<IEnumerable<Domain.Entity.Article>>> GetAll();
    }
}
