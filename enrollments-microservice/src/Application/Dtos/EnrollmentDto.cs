using System.ComponentModel.DataAnnotations;
namespace enrollments_microservice.Application.Dtos;
public class EnrollmentDto
{
    public string? Id { get; set; } = string.Empty;
    public string? StudentId { get; set; } = string.Empty;
    public string? SchoolId { get; set; } = string.Empty;
    public string? FullName { get; set; } = string.Empty;
    public int? AcademicPerformance { get; set; } = 0;
    [Required]
    public int Credits { get; set; }
    [Required]
    public List<CourseDto>? Courses { get; set; } = new List<CourseDto>();
    public string? SchoolName { get; set; } = string.Empty;
}