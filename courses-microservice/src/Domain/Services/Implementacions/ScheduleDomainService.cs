using Domain.Courses;
using Domain.Schedules.Services.Interfaces;
using Domain.Schedules.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Schedules
{
    public class ScheduleDomainService : IScheduleDomainService
    {
        private readonly ICourseRepository _courseRepository;

        public ScheduleDomainService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public bool ValidateScheduleEntries(IEnumerable<ScheduleEntry> entries, Course course)
        {
            // Calcular el total de horas de las entradas
            var totalHours = entries.Sum(entry => (entry.EndTime - entry.StartTime).TotalHours);

            // Validar si el total de horas coincide con las horas del curso
            return totalHours == course.Hours;
        }
    }
}
