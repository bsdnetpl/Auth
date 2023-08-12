using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.DB
{
    public class ConnectDB : DbContext
    {
        public ConnectDB(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }

    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.email)
            .IsRequired();

        modelBuilder.Entity<User>()

            .HasIndex(e => e.email)
            .IsUnique();

            modelBuilder.Entity<Role>()
            .Property(x => x.Name)
            .IsRequired();

    }
    }
}
