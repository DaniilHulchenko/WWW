using Microsoft.EntityFrameworkCore;
using WWW.Domain.Entity;
using WWW.DAL;
namespace WWW.DAL {
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
            //ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<Event> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Date> Date { get; set; }

    }
}
