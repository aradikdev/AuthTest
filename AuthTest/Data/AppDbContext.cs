using AuthTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthTest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "admin" });
        modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = "user" });
        modelBuilder.Entity<User>().HasData(new User { Id = 1, Login="admin", Name="Super Admin", Email = "admin@admin.admin", Password = "admin@admin.admin", RoleId = 1 });
    }



    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }


}
