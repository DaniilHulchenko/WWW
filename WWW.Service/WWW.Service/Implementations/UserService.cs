using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Service.Interfaces;

namespace WWW.Service.Implementations
{
    public class UserService : IBaseService<User>
    {
        public Task<bool> Create(User category)
        {
            throw new Exception();
        }

        public Task<bool> Delete(int i)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<User>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
