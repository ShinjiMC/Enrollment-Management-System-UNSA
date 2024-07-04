using System.ComponentModel.DataAnnotations;

namespace course_microservice.models
{
    public class WeekDayDto
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string? Name { get; set; }
    }
}
