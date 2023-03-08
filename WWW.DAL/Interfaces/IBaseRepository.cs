using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL;
namespace WWW.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        T GetValueByID(int id);
        Task<IEnumerable<T>> GetAll();
        bool Delete(T entity);
    }
}
