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
    public class EntityBaseRepository<T> : IBaseRepository<T> where T : DbBase
    {
        private readonly ApplicationDbContext _db;
        public EntityBaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().ToArrayAsync();
            
        }

        public IQueryable<T> GetALL()
        {
            return _db.Set<T>();
        }

        //public Task<T> GetValueByID(int id) => _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        public Task<T> GetValueByID(int id) { throw new Exception(); }
    }
}
