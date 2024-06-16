using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using course_microservice.models;

namespace course_microservice.DTOs
{
    public class ScheduleDto
    {
        [Key]
        public int ScheduleID { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [Required]
        [ForeignKey("WeekDay")]
        public int DayOfWeekID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(2)]
        public string? Group { get; set; }

        [Required]
        [StringLength(200)]
        public string? TeacherFullName { get; set; }

        public int Quantity { get; set; }

        public CourseDto? Course { get; set; }

        public WeekDayDto? WeekDay { get; set; }
    }
}
