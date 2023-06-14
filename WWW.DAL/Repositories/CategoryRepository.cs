using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<Category>> GetNotEmptyCategory() {         
            string query = "select * from Categories " +
                    "where exists (" +
                    "select * from Articles where Articles.CategoryID = Categories.Id" +
                    ");";

            return _db.Categories.FromSqlRaw(query).AsQueryable();
        }


        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _db.Categories.ToListAsync();
        }


        public async Task<Category> GetValueByID(int id)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Create(Category entity)
        {
            await _db.Categories.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Category entity)
        {
            _db.Categories.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<Category> GetALL()
        {
            return _db.Categories;
        }

    }
}
