using FunkoProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FunkoProject.Data
{
    public class AppDbContext : DbContext
    {
        private string _connectionString =
            "Server=localhost;Database=FiguresDb;Trusted_Connection=True;TrustServerCertificate=True";

        public DbSet<Figure> Figures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();
            modelBuilder.Entity<Figure>()
                .Property(f => f.Handle)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}