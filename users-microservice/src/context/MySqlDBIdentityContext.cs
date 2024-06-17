using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using users_microservice.models;

namespace users_microservice.context;


public class MySqlIdentityContext : IdentityDbContext<ApplicationUser>
{
    public MySqlIdentityContext(DbContextOptions<MySqlIdentityContext> options) : base(options) { }
    public DbSet<StudentModel> StudentsModel { get; set; } = null!;
}
