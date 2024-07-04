using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using course_microservice.models;

namespace course_microservice.DTOs
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
        public string Group { get; set; }

        [Required]
        public int Year { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string TeacherFullName { get; set; } = string.Empty;

        public int Capacity { get; set; }


        public CourseModel? Course { get; set; }

        public WeekDayModel? WeekDay { get; set; }

        public SchoolModel? School { get; set; }


    }
}