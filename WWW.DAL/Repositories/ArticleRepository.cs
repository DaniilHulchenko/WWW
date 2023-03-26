using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task<bool> AddTags(Article article,Tags tags)
        {
            if (article.Tags == null) 
                article.Tags = new List<Tags> { tags };
            else
                article.Tags.Add(tags);

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Create(Article entity)
        {
            await _db.Articles.AddAsync(entity);   
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Article entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _db.Articles.ToListAsync();
        }
        public IQueryable<Article> GetALL()
        {
            return _db.Articles;
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

        public async Task<Article> GetValueByID(int id)
        {
            return await _db.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
