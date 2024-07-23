using Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            
            // Configura la clave primaria
            builder.HasKey(c => c.CourseId);

            // Configura la conversión para CourseId
            builder.Property(c => c.CourseId).IsRequired();

            // Configura propiedades adicionales
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Credits).IsRequired();
            builder.Property(c => c.Hours).IsRequired();
            builder.Property(c => c.Active).IsRequired();

            // Configura la propiedad Semester como un objeto de valor
            builder.OwnsOne(c => c.Semester, semesterBuilder =>
            {
                semesterBuilder.Property(s => s.Value).HasMaxLength(2).IsRequired();
            });

            // Configura la propiedad School como un objeto de valor
            builder.Property(c => c.SchoolId).IsRequired();

            // Configura las relaciones (por ejemplo, si es necesario configurar FK o relaciones)
        }
    }
}
