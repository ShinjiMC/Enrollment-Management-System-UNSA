using System.ComponentModel.DataAnnotations;

namespace course_microservice.models
{
    public class ScheduleModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public int WeekDayID { get; set; }

        [Required]
        public int SchoolID { get; set; }

        [Required]
        [StringLength(2)]
        public string Group { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string TeacherFullName { get; set; } = string.Empty;

        public CourseModel? Course { get; set; }

        public WeekDayModel? WeekDay { get; set; }

        public SchoolModel? School { get; set; }


    }
}