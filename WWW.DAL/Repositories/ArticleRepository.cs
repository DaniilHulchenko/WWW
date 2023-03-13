using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;

namespace WWW.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private ApplicationDbContext _db;
        public ArticleRepository(ApplicationDbContext db) {
            _db=db;
        }

        public bool Create(Article entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Article entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _db.Articles.ToListAsync();
        }

        public async Task< IEnumerable <Article >> GetByCategoryName(string CatName)
        {
            if (CatName != "")
            {
                Category CatId = await _db.Categories.FirstOrDefaultAsync(c => c.Name == CatName);

                if (CatId != null)
                    return await _db.Articles.Where(a => a.CategoryID == CatId.Id).ToListAsync();
                else
                {
                    return await _db.Articles.ToListAsync();
                }
            }
            else
            {
                return await _db.Articles.ToListAsync();
            }

        }

        public Article GetValueByID(int id)
        {
            return _db.Articles.SingleOrDefault(a => a.Id == id);
        }

        Task<bool> IBaseRepository<Article>.Create(Article entity)
        {
            throw new NotImplementedException();
        }

    }
}
