using Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CoursePrerequisiteConfiguration : IEntityTypeConfiguration<CoursePrerequisite>
    {
        public void Configure(EntityTypeBuilder<CoursePrerequisite> builder)
        {
            builder.ToTable("CoursePrerequisites");

            // Configura la clave primaria compuesta
            builder.HasKey(cp => new { cp.CourseId, cp.PrerequisiteCourseId });

            // Configura la conversión para CourseId
            builder.Property(cp => cp.CourseId)
                .HasConversion(
                    courseId => courseId.Value,
                    value => new CourseId(value)
                );
            builder.Property(cp => cp.PrerequisiteCourseId)
                .HasConversion(
                    courseId => courseId.Value,
                    value => new CourseId(value)
                );

            // Configura la relación entre Course y CoursePrerequisite
            builder.HasOne(cp => cp.Course)
                .WithMany()  // Ajusta esta relación según tu diseño
                .HasForeignKey(cp => cp.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Configura el comportamiento de eliminación si es necesario

            builder.HasOne(cp => cp.PrerequisiteCourse)
                .WithMany()  // Ajusta esta relación según tu diseño
                .HasForeignKey(cp => cp.PrerequisiteCourseId)
                .OnDelete(DeleteBehavior.Restrict); // Configura el comportamiento de eliminación si es necesario
        }
    }
}
