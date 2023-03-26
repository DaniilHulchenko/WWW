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
    public class UserRepository : IBaseRepository<User>
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public IQueryable<User> GetALL()
        {
            return _db.Users;
        }

        public async Task<User> GetValueByID(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
