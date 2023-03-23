using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Service.Interfaces;
using WWW.DAL.Interfaces;
using WWW.Domain.Enum.StatusCode;

namespace WWW.Service.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<BaseResponse<IEnumerable<Article>>> GetAll()
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

        public async Task<BaseResponse<IEnumerable<Article>>> GetByCategoryName(string CatName)
        {
            var BaseResponse = new BaseResponse<IEnumerable<Article>>();
            try
            {
                BaseResponse.Data = await _articleRepository.GetByCategoryName(CatName);
                BaseResponse.StatusCode = StatusCode.OK;
            }
            catch(Exception ex) { 
                BaseResponse.ErrorDescription += ex.Message;
                BaseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return BaseResponse;
        }
    }
}
