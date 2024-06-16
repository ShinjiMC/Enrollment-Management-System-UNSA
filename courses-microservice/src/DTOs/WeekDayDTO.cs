using System.ComponentModel.DataAnnotations;

namespace course_microservice.models
{
    public class WeekDayDto
    {
        [Key]
        public int DayOfWeekID { get; set; }

        [Required]
        [StringLength(20)]
        public string? Name { get; set; }
    }
}
