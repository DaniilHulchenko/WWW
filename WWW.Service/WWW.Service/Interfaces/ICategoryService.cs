
using WWW.Domain.Entity;
using WWW.Domain.Response;

namespace WWW.Service.Interfaces
{
    public interface ICategoryService
    {
        public Task<IBaseResponse<IEnumerable<Domain.Entity.Category>>> GetAll();
        public Task<bool> Create(Category category);
        public bool DeleteById(int Id);
    }
}
