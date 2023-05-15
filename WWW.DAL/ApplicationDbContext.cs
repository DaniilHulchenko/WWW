using Microsoft.EntityFrameworkCore;
using WWW.Domain.Entity;
using WWW.DAL;
using System.Data;

namespace WWW.DAL {
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Date> Date { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.UseSqlServer("");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<User>(builder =>
        //    {
        //        builder.ToTable("Users").HasKey(x => x.Id);

        //        builder.HasData(new User[] {
        //            new User()
        //            {
        //                Id = 1,
        //                FirstName = "Admin",
        //                LastName = "Admin",
        //                NickName = "Admin",
        //                Email = "-",
        //                Password = HashPasswordHelper.HashPassowrd("admin"),
        //                Role = WWW.Domain.Enum.UserRole.Admin
        //            },
        //            new User()
        //            {
        //                Id = 1,
        //                FirstName = "ticketmaster",
        //                LastName = "ticketmaster",
        //                NickName = "ticketmaster",
        //                Email = "-",
        //                Password = HashPasswordHelper.HashPassowrd("ticketmaster"),
        //                Role = WWW.Domain.Enum.UserRole.Admin
        //            }
        //        });
        //    });
        //}
    }
}
