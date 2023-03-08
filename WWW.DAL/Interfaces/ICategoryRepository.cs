using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Entity;
using WWW.DAL;

namespace WWW.DAL.Interfaces
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
        //Category GetCategoryByName(string name);
        IEnumerable<Category> GetNotEmptyCategory();
    }
    
}
