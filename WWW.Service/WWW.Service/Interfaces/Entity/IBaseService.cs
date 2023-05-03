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
        Task<BaseResponse<IEnumerable<T>>> GetAll();
        Task<bool> Create(T category);
        Task<bool> Delete(int id);
    }
}
