using System.ComponentModel.DataAnnotations;

namespace course_microservice.DTOs
{
    public class ScheduleDto
    {
        public int ID { get; set; }

        public int CourseID { get; set; }

        public int WeekDayID { get; set; }

        public int SchoolID { get; set; }

        public string Group { get; set; } = string.Empty;

        public int Year { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [StringLength(200)]
        public string TeacherFullName { get; set; } = string.Empty;

        public CourseDto? Course { get; set; }

        public WeekDayDto? WeekDay { get; set; }

        public SchoolDto? School { get; set; }
    }
}
