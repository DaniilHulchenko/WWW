using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Domain.Enum;
using WWW.Service.Interfaces;
using WWW.DAL.Interfaces;

namespace WWW.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository articleRepository)
        {
            _categoryRepository = articleRepository;
        }

        public async Task<bool> Create(Category category)
        {
            return await _categoryRepository.Create(category);
          
        }


    public bool DeleteById(int id)
    {
        var category = _categoryRepository.GetValueByID(id);
        return _categoryRepository.Delete(category);
    }

    public async Task<BaseResponse<IEnumerable<Category>>> GetAll()
    {
        BaseResponse<IEnumerable<Category>> BaseResponse = new BaseResponse<IEnumerable<Category>>();
        try{
            var Articles = await _categoryRepository.GetAll();
            if (!Articles.Any())
            {
                BaseResponse.ErrorDescription = "Found 0 elements";
                //BaseResponse.StatusCode = StatusCode.OK;
            }
            else
            {
                BaseResponse.Data = Articles;
                //BaseResponse.StatusCode = StatusCode.OK;
            }
                return BaseResponse;
        }
        catch(Exception ex)
        {
            return new BaseResponse<IEnumerable<Category>>()
            {
                ErrorDescription = $"[Articles.GetAll]:{ex.Message}",
            };
        }
    }
    }
}
