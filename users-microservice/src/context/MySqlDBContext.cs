using Microsoft.EntityFrameworkCore;
using users_microservice.models;
namespace users_microservice.repositories;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
    
    public DbSet<UserModel> Users { get; set; } // Debes tener una clase User que represente tu entidad de usuario

}