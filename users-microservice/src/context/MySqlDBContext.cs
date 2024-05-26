using Microsoft.EntityFrameworkCore;
using users_microservice.models;
namespace users_microservice.repositories;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
    
    public DbSet<UserModel> Users { get; set; } 
    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Course> Courses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Student>("STUDENT")
            .HasValue<Admin>("ADMIN");
    }
}