using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using users_microservice.models;

namespace users_microservice.context;


public class MySqlIdentityContext : IdentityDbContext<ApplicationUser>
{
    public MySqlIdentityContext(DbContextOptions<MySqlIdentityContext> options) : base(options) { }
    
    public DbSet<StudentModel> StudentsModel { get; set; } = null!;

    // public DbSet<ApplicationUser> ApplicationUsers { get; set; } // DbSet para ApplicationUser
    public DbSet<Student> Students { get; set; } // DbSet para Student

    public DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuración de herencia TPT
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers"); // Tabla base
        builder.Entity<Student>().ToTable("Students"); // Tabla para Student

        
    }
}
