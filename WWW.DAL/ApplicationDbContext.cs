using Microsoft.EntityFrameworkCore;
using WWW.Domain.Entity;
using WWW.DAL;
namespace WWW.DAL {
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tags> Tags { get; set; }

    }
}
