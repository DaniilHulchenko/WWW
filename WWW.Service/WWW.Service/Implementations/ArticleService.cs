using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Service.Interfaces;
using WWW.DAL.Interfaces;
using WWW.Domain.Enum;
using Microsoft.EntityFrameworkCore;

using WWW.DAL.Repositories;
using WWW.Domain.ViewModels.Article;

namespace WWW.Service.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IAccountRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ArticleService(IArticleRepository articleRepository, IAccountRepository userRepository, ICategoryRepository categoryRepository )
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<BaseResponse<IEnumerable<Article>>> GetAll()
        {
            var BaseResponse = new BaseResponse<IEnumerable<Article>>();
            try {
                var Articles = await _articleRepository.GetAll();
                if (Articles.Count() == 0)
                {
                    BaseResponse.ErrorDescription = "Found 0 elements";
                    BaseResponse.StatusCode = (StatusCode)StatusCode.OK;
                }
                else
                {
                    BaseResponse.Data = Articles;
                    BaseResponse.StatusCode = (StatusCode)StatusCode.OK;
                }
                return BaseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Article>>()
                {
                    ErrorDescription = $"[Articles.GetAll]:{ex.Message}",
                };
            }
        }
        public async Task<BaseResponse<IEnumerable<Article>>> GetByCategoryName(string CatName)
        {
            BaseResponse<IEnumerable<Article>> BaseResponse = new BaseResponse<IEnumerable<Article>>();
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
        public async Task<BaseResponse<Article>> GetById(int id)
        {
            try
            {
                var data = await _articleRepository.GetALL().FirstOrDefaultAsync(a => a.Id == id);
                return new BaseResponse<Article>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    ErrorDescription = ex.Message,
                };
            }


        }

        public async Task<bool> Create(ArticleCreateViewModal entity)
        {
            Article data = new Article()
            {
                Title = entity.Title,
                ShortDescription = entity.ShortDescription,
                Description = entity.Description,
                Published = entity.Published,
                Category = await _categoryRepository.GetValueByID(entity.Category),

                Location = new Location() { location = entity.Location },
                Date = new Date() { Date_of_Creation = entity.DateOfArticle },

            };
            if (entity.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await entity.Picture.CopyToAsync(memoryStream);
                    data.Picture = new Picture() { picture = memoryStream.ToArray() };
                }
            }

            if (!_articleRepository.GetALL().Any())
                data.slug = entity.Title.ToLower().Replace(' ', '-') + "-" + (0);
            else
                data.slug = entity.Title.ToLower().Replace(' ', '-') + "-" + (_articleRepository.GetALL().OrderBy(a => a.Id).Last().Id + 1);
            data.Autor = await _userRepository.GetALL().FirstAsync();
            data.IsFavorite = true;

            return await _articleRepository.Create(data);
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddTag(Article article,Tags tags)
        {
            await _articleRepository.AddTags(article, tags);
            return true;
        }



                public Task<bool> Create(Article category)
        {
            throw new NotImplementedException();
        }
    }
}
