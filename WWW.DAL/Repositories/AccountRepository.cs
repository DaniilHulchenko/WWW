using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.Domain.Entity;

namespace WWW.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext _db;
        public AccountRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(User entity)
        {
            if (entity.Id != 0)
            {
                string sql = ("USE www; " +
                    "SET IDENTITY_INSERT Users ON; " +
                    "INSERT INTO Users (Id, NickName, Email, Avatar, Role) " +
                    $"VALUES ({entity.Id}, '{entity.NickName}', '{entity.Email}', @Avatar, 0) " +
                    "SET IDENTITY_INSERT Users OFF;");

                _db.Database.ExecuteSqlRaw(sql, new SqlParameter("@Avatar", SqlDbType.VarBinary) { Value = entity.Avatar });

                await _db.SaveChangesAsync();
            }
            else {
                await _db.Users.AddAsync(entity);
                await _db.SaveChangesAsync();
            }
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
