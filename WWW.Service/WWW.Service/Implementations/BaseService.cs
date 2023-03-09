using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;

namespace WWW.Service.Implementations
{
    public class BaseService<T> where T : IBaseRepository<T>
    {
        private readonly T _Repository;

        public BaseService(T Repository)
        {
            _Repository = Repository;
        }

        public bool DeleteById(int id)
        {
            var category = _Repository.GetValueByID(id);
            return _Repository.Delete(category);
        }


    }
}
