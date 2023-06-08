using Microsoft.EntityFrameworkCore;
using WWW.Domain.Entity;
using WWW.DAL;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using Castle.Core.Configuration;

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
        public DbSet<EventDates> Date { get; set; }
        public DbSet<User_Details> User_Details { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasOne(a => a.Autor).WithMany(u=>u.AutorEvent);
            modelBuilder.Entity<Article>().HasMany(a => a.User).WithMany(u=>u.FavEvent);
            //modelBuilder.Entity<User_Details>()
            //    .Ignore(x => x.Id);
            //modelBuilder.Entity<Article>()
            //    .HasMany(a => a.UserFavorite)
            //    .WithMany(u => u.UserFavoriteEvents)
            //    .UsingEntity(j => j.ToTable("UserFavoriteArticles"));
            User[] users=new [] { 
                new User { Id=1, NickName="admin",Email="admin@gmail.com",Role=Domain.Enum.UserRole.Admin },
                new User { Id=2, NickName="TicketMaster",Email="ticketmaster@gmail.com",Role=Domain.Enum.UserRole.Moderator },
            };
            User_Details[] user_Details = new[]{
                new User_Details{ UserID=1, Introdaction="Admin Account", Password=HashPasswordHelper.HashPassowrd("admin320")},
                new User_Details{ UserID=2, Introdaction="TicketMaster Official Account",Password=HashPasswordHelper.HashPassowrd("ticketmaster320")},
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<User_Details>().HasData(user_Details);

        }
    }


    public class HashPasswordHelper
    {
        public static string HashPassowrd(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
