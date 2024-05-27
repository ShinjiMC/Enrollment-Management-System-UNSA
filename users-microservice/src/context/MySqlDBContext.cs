using Microsoft.EntityFrameworkCore;
using users_microservice.models;
namespace users_microservice.repositories;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
    public DbSet<StudentModel> Students { get; set; }
    public DbSet<AdminModel> Admins { get; set; }
    public DbSet<Course> Courses { get; set; }
}