using System.ComponentModel.DataAnnotations;
using users_microservice.Domain.Entities;

namespace users_microservice.Application.Dtos;

public class StudentDto
{
    public string? Id { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Cui { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    // You can add it if necessary, for example:
    public List<string> CourseIds { get; set; } = new List<string>();

    public AcademicPerformance? AcademicPerformance { get; set; }
    public int? Credit { get; set; }
    public string? SchoolId { get; set; } = string.Empty;
}