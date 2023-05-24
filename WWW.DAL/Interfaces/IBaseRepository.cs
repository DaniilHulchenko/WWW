using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL;
namespace WWW.DAL.Interfaces
{
    public interface IBaseRepository<T> where T:class//// зробити дженерики типу create ітд через базовий тип 
    {
        public Task<bool> Create(T entity);
        public Task<bool> Delete(T entity);
        public Task<T> GetValueByID(int id);
        public Task<IEnumerable<T>> GetAll();
        public IQueryable<T> GetALL();
    }
}
