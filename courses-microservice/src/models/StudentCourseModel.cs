using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace course_microservice.DTOs
{
    public class StudentCourseModel
    {
        [Key]
        public int StudentCourseID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("^[A-B]$", ErrorMessage = "Semester must be 'A' or 'B'.")]
        public char Semester { get; set; }

        [Required]
        [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100.")]
        public int Year { get; set; }

        public CourseModel? Course { get; set; }
    }
}
