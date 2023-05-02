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
    public class DateRepository : IDateRepository
    {
        private readonly ApplicationDbContext _db;
        public DateRepository(ApplicationDbContext db) {
            _db = db;
        }
        public async Task<bool> Create(Date entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Date entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Date>> GetAll()
        {
            return await _db.Date.ToListAsync();
        }

        public IQueryable<Date> GetALL()
        {
            return _db.Date;
        }

        public async Task<Date> GetValueByID(int id) => await _db.Date.FirstOrDefaultAsync(d => d.ArticleID == id);
    }
}
