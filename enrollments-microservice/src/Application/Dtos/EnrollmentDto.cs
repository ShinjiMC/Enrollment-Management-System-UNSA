using System.ComponentModel.DataAnnotations;
namespace enrollments_microservice.Application.Dtos;
public class EnrollmentDto
{
    public string? Id { get; set; } = null;
    public string? StudentId { get; set; } = null;
    public string? SchoolId { get; set; } = null;
    public string? FullName { get; set; } = null;
    public int? AcademicPerformance { get; set; } = null;
    public int? Credits { get; set; } = null;
    [Required]
    public List<CourseDto>? Courses { get; set; } = null;
    public string? SchoolName { get; set; } = null;
}