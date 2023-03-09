using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;



namespace WWW.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> GetNotEmptyCategory() {         
            string query = "select * from Categories " +
                    "where exists (" +
                    "select * from Articles where Articles.CategoryID = Categories.Id " +
                    "); ";
            return _db.Categories.FromSqlRaw(query);
        }


        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _db.Categories.ToListAsync();
        }


        public Category GetValueByID(int id)
        {
            return _db.Categories.First(c => c.Id == id);
        }

        public async Task<bool> Create(Category entity)
        {
            await _db.Categories.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public bool Delete(Category entity)
        {
            _db.Categories.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
