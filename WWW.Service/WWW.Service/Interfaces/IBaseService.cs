using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Entity;
using WWW.Domain.Response;

namespace WWW.Service.Interfaces
{
    public interface IBaseService <T>  
    {
        public Task<BaseResponse<IEnumerable<T>>> GetAll();
        public Task<bool> Create(T category);
        public Task<bool> Delete(int id);
    }
}
