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
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<UserFigure> UserFigures { get; set; }

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
            modelBuilder.Entity<Attachment>()
                .Property(f => f.UserId)
                .IsRequired();
            modelBuilder.Entity<Attachment>()
                .HasOne(f => f.User)
                .WithMany(u => u.Attachments)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserFriend>()
                .HasKey(uf => new { uf.UserId, uf.FriendId });
            modelBuilder.Entity<UserFriend>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserFriend>()
                .HasOne(uf => uf.Friend)
                .WithMany(u => u.FriendOf)
                .HasForeignKey(uf => uf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserFigure>()
                .HasOne(uf => uf.OwningUser)
                .WithMany(u => u.UserFigures) // jeśli masz kolekcję w User
                .HasForeignKey(uf => uf.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}