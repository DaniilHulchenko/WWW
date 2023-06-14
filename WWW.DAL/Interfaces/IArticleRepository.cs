using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Entity;
using WWW.DAL;
using WWW.Domain.Response;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace WWW.DAL.Interfaces
{
    public interface IArticleRepository:IBaseRepository<Article>
    {
        IQueryable<Article> SearchByTitle(string searchTerm);
        Task<IQueryable<Article>> GetByCategoryName(string CatName);
        Task<bool> AddTags(Article article,Tags tags);
        public IQueryable<Article> SearchByTitle(IQueryable<Article> article, string searchTerm);
    }
}
