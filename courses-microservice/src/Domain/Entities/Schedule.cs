using Domain.Courses;
using Domain.Schedules.ValueObjects;

namespace Domain.Schedules
{
    public class Schedule
    {
        public Guid ScheduleId { get; private set; } // Identificador único para el horario
        public int Year { get; private set; } // Año en que se dicta el curso
        public string SchoolId { get; private set; }
        public Guid CourseId { get; private set; }
        public List<ScheduleEntry> Entries { get; private set; } // Horario detallado
        public ScheduleDetails ScheduleDetails { get; private set; } // Información del curso y profesor
        public Course Course {get; set; }
        private Schedule(){}
        // Constructor para inicializar todos los campos
        public Schedule(
            Guid scheduleId,
            int year,
            string schoolId,
            Guid courseId,
            List<ScheduleEntry> entries,
            ScheduleDetails scheduleDetails)
        {
            ScheduleId = scheduleId;
            Year = year;    
            SchoolId = schoolId;
            CourseId = courseId;
            Entries = entries;
            ScheduleDetails = scheduleDetails;
        }

        public void Update(
            Guid courseId,
            int year,
            string schoolId,
            ScheduleDetails scheduleDetails,
            List<ScheduleEntry> entries)
        {
            CourseId = courseId;
            Year = year;
            SchoolId = schoolId;
            ScheduleDetails = scheduleDetails;
            Entries = entries;
        }
    }
}
