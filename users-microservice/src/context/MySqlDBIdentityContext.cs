using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using users_microservice.models;

namespace users_microservice.context;


public class MySqlIdentityContext : IdentityDbContext<ApplicationUser>
{
    public MySqlIdentityContext(DbContextOptions<MySqlIdentityContext> options) : base(options) { }
    
    public DbSet<Student> StudentsModel { get; set; } // DbSet para Student
    public DbSet<Admin> AdminModel { get; set; } // DbSet para Student

    public DbSet<StudentCourse> StudentCourses { get; set; }  // DbSet student id with courser id

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuración de herencia TPT
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers"); // Tabla base
        builder.Entity<Student>().ToTable("StudentsModel"); // Tabla para Student
        builder.Entity<Admin>().ToTable("AdminModel"); // Tabla para Admin

        builder.Entity<StudentCourse>().ToTable("StudentCourse"); // Tabla para Admin
  
    }
}
