using System.ComponentModel.DataAnnotations;

namespace course_microservice.DTOs
{
    public class SchoolModel
    {
        [Key]
        public int ID { get; set; }

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
