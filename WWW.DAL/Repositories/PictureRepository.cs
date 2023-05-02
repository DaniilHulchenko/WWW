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
    public class PictureRepository : IPictureRepository
    {
        private readonly ApplicationDbContext _db;
        public PictureRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Picture entity)
        {
            await _db.Picture.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Picture entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Picture>> GetAll()
        {
            return await _db.Picture.ToListAsync();
        }

        public IQueryable<Picture> GetALL()
        {
            return _db.Picture;
        }

        public async Task<Picture> GetValueByID(int id)
        {
            return await _db.Picture.FirstAsync(p=>p.PictureID == id);
        }
    }
}
