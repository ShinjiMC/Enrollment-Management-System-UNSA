using System.ComponentModel.DataAnnotations;

namespace course_microservice.DTOs
{
    public class CourseModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^[A-B]$", ErrorMessage = "Semester must be 'A' or 'B'.")]
        public char Semester { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Credits must be between 1 and 50.")]
        public int Credits { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Hours { get; set; } 
    }
}
