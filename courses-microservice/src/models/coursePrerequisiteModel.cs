using System.ComponentModel.DataAnnotations;
namespace course_microservice.models
{
    public class CoursePrerequisiteModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public int PrerequisiteCourseID { get; set; }

        public CourseModel? Course { get; set; }
        public CourseModel? PrerequisiteCourse { get; set; }
    }
}