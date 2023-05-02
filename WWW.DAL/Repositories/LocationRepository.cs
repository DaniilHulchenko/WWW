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
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Location entity)
        {
            await _db.Location.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Location entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _db.Location.ToListAsync();
        }

        public IQueryable<Location> GetALL()
        {
            return _db.Location;
        }

        public async Task<Location> GetValueByID(int id)
        {
            return await _db.Location.FirstAsync(l => l.LocationID == id);
        }
    }
}
