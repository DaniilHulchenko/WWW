using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;

namespace WWW.DAL.Repositories
{
    public class TagRepository: IBaseRepository<Tags>
    {
        private ApplicationDbContext _db;

        public TagRepository(ApplicationDbContext db) {
            _db = db;
        }

        public async Task<bool> Create(Tags entity)
        {
            await _db.Tags.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Tags entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tags>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tags> GetALL()
        {
            return _db.Tags;
        }

        public Task<Tags> GetValueByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
