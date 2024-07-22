using Microsoft.EntityFrameworkCore;
using users_microservice.Domain.Entities;
using users_microservice.Domain.ValueObjects;

namespace users_microservice.Repository.Data
{
    public class MySqlIdentityContext : DbContext
    {
        public MySqlIdentityContext(DbContextOptions<MySqlIdentityContext> options) : base(options) { }
        
        public DbSet<UserModel> Users { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<CourseModel> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity tables
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<StudentModel>().ToTable("Students");
            modelBuilder.Entity<AdminModel>().ToTable("Admins");
            modelBuilder.Entity<CourseModel>().ToTable("Courses");

            // Configure value objects for StudentModel
            modelBuilder.Entity<StudentModel>()
                .OwnsOne(s => s.StudentData, studentData =>
                {
                    studentData.Property(sd => sd.Cui).HasMaxLength(50);
                    studentData.Property(sd => sd.SchoolId).HasMaxLength(50).IsRequired(false);
                });

            // Configure value objects for CourseModel
            modelBuilder.Entity<CourseModel>()
                .OwnsOne(c => c.CourseData, courseData =>
                {
                    courseData.Property(cd => cd.CourseId).HasMaxLength(50);
                    courseData.Property(cd => cd.Status).HasMaxLength(50);
                });

            // Configure one-to-many relationship for courses
            modelBuilder.Entity<StudentModel>()
                .HasMany(s => s.StudentCourses)
                .WithOne() // Assuming CourseInfo is not an entity but a value object
                .HasForeignKey("StudentId"); // Ensure this matches the actual foreign key in the Course table

            // Ensure CourseInfo and StudentInfo are not registered as separate entities
            // modelBuilder.Ignore<CourseInfo>();
            // modelBuilder.Ignore<StudentInfo>();
        }
    }
}
