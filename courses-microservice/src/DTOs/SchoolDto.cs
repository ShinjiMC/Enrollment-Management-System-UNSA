using System.ComponentModel.DataAnnotations;

namespace course_microservice.DTOs
{
    public class SchoolDto
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Faculty { get; set; }

        public string? Area { get; set; }

        public DateTime FoundationDate { get; set; }
    }
}


