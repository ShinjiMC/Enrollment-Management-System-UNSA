using System.ComponentModel.DataAnnotations;

namespace enrollments_microservice.Application.Dtos;

public class CourseDto
{
    [Required]
    public string? Id { get; set; } = string.Empty;
    [Required]
    public string? Group { get; set; } = string.Empty;
}