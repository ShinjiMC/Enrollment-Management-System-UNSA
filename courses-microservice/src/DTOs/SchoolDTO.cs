using System.ComponentModel.DataAnnotations;

namespace course_microservice.DTOs
{
    public class SchoolDto
    {
        [Key]
        public int SchoolID { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Faculty { get; set; }

        [StringLength(100)]
        public string? Area { get; set; }

        public DateTime FoundationDate { get; set; }
    }
}
