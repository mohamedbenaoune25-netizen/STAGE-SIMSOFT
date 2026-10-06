using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<VisitorRequest> VisitorRequests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuration de données par défaut (Data Seeding)
            // Hachage du mot de passe 'admin123'
            var initialPasswordHash = "$2a$11$vWeWy8BIQVJ6LhIz1tiPgeGe0Xt0Qw4SOQYXtK5pMsFjUKtDO7x/u";

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FullName = "Admin Principal",
                Email = "mohamed.benaoune25@gmail.com",
                PasswordHash = initialPasswordHash,
                Role = "Admin",
                CreatedAt = new System.DateTime(2026, 8, 1, 0, 0, 0, System.DateTimeKind.Utc),
                IsActive = true
            });
        }
    }
}
