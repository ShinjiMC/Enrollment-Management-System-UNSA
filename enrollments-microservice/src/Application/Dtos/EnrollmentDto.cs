using System.ComponentModel.DataAnnotations;
namespace enrollments_microservice.Application.Dtos;
public class EnrollmentDto
{
    public string? Id { get; set; } = string.Empty;
    public string? StudentId { get; set; } = string.Empty;
    public string? SchoolId { get; set; }
    [Required]
    public string? FullName { get; set; } = string.Empty;
    [Required]
    public int AcademicPerformance { get; set; }
    [Required]
    public int Credits { get; set; }
    [Required]
    public List<CourseDto>? Courses { get; set; } = new List<CourseDto>();
    [Required]
    public string? SchoolName { get; set; } = string.Empty;
}