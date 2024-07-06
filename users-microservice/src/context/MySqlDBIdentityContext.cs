using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using users_microservice.models;

namespace users_microservice.context
{
    public class MySqlIdentityContext : IdentityDbContext<UserModel>
    {
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<StudentUser> StudentUsers { get; set; }
        public DbSet<CourseModel> Courses { get; set; }

        public MySqlIdentityContext(DbContextOptions<MySqlIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure AdminUser
            builder.Entity<AdminUser>(entity =>
            {
                entity.ToTable("AspNetUsers"); // Specify the table name if needed
            });
            builder.Entity<StudentUser>(entity =>
            {
                entity.ToTable("AspNetUsers"); // Especificar el nombre de la tabla si es necesario
            });
            builder.Entity<CourseModel>(entity =>
            {
                entity.ToTable("Courses"); // Especificar el nombre de la tabla si es necesario
                entity.HasOne(c => c.StudentUser)
                      .WithMany(u => u.Courses)
                      .HasForeignKey(c => c.StudentUserId);
            });
        }
    }
}
