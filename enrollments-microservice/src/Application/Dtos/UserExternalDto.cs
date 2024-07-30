using System.Text.Json.Serialization;

namespace enrollments_microservice.Application.Dtos;

public class UserExternalDto
{
    [JsonPropertyName("fullName")]
    public string? FullName { get; set; } = string.Empty;
    [JsonPropertyName("courseIds")]
    public List<string>? Courses { get; set; } = new List<string>();
    [JsonPropertyName("academicPerformance")]
    public int AcademicPerformance { get; set; }
    [JsonPropertyName("credit")]
    public int Credits { get; set; }
}