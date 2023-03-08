using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Service.Interfaces;



namespace WWW.S.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Task<BaseResponse<IEnumerable<Article>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
