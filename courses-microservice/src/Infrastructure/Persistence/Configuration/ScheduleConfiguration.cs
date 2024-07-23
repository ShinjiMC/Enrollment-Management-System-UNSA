using Domain.Schedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Courses;

namespace Infrastructure.Persistence.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules");

            builder.HasKey(s => s.ScheduleId);

            builder.Property(s => s.ScheduleId).IsRequired();

            builder.Property(s => s.Year).IsRequired();
            builder.Property(s => s.SchoolId).IsRequired();

            // Configura la relación con Course
            builder.HasOne(s => s.Course)
                .WithMany() // o WithMany(c => c.Schedules) si tienes una propiedad en Course
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configura el objeto de valor ScheduleDetails
            builder.OwnsOne(s => s.ScheduleDetails, detailsBuilder =>
            {
                detailsBuilder.Property(d => d.Group)
                    .HasMaxLength(50)
                    .IsRequired();

                detailsBuilder.Property(d => d.ProfessorId)
                    .IsRequired();
            });

            // Configura la propiedad ScheduleEntry como un objeto de valor
            builder.OwnsMany(s => s.Entries, entriesBuilder =>
            {
                entriesBuilder.WithOwner().HasForeignKey("ScheduleId");

                entriesBuilder.Property(e => e.DayOfWeek).IsRequired();
                entriesBuilder.Property(e => e.StartTime).IsRequired();
                entriesBuilder.Property(e => e.EndTime).IsRequired();
            });
        }
    }
}
