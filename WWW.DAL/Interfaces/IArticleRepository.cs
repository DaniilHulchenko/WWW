using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Entity;
using WWW.DAL;

namespace WWW.DAL.Interfaces
{
    public interface IArticleRepository:IBaseRepository<Article>
    {
        Task<IEnumerable<Article>> GetByCategoryName(string CatName);
    }
}
