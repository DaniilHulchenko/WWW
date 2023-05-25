using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Entity;
using WWW.DAL;
using WWW.Domain.Response;
using System.Runtime.CompilerServices;

namespace WWW.DAL.Interfaces
{
    public interface IArticleRepository:IBaseRepository<Article>
    {
        Task<IQueryable<Article>> GetByCategoryName(string CatName);
        Task<bool> AddTags(Article article,Tags tags);
    }
}
