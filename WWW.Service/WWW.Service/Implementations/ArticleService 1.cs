using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Service.Interfaces;
using WWW.DAL.Interfaces;
using Grpc.Core;

namespace WWW.Service.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Article>>> GetAll()
        {
            var BaseResponse = new BaseResponse<IEnumerable<Article>>();
            try{
                var Articles = await _articleRepository.GetAll();
                if (Articles.Count() == 0)
                {
                    BaseResponse.ErrorDescription = "Found 0 elements";
                    BaseResponse.StatusCode = (Domain.Enum.StatusCode.StatusCode)StatusCode.OK;
                }
                else
                {
                    BaseResponse.Data = Articles;
                    BaseResponse.StatusCode = (Domain.Enum.StatusCode.StatusCode)StatusCode.OK;
                }
                    return BaseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Article>>()
                {
                    ErrorDescription = $"[Articles.GetAll]:{ex.Message}",
                };
            }
        }
    }
}
