using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace users_microservice.context;

public class MySqlIdentityContext : IdentityDbContext<ApplicationUser>
{
    public MySqlIdentityContext(DbContextOptions<MySqlIdentityContext> options) : base(options) { }
}
