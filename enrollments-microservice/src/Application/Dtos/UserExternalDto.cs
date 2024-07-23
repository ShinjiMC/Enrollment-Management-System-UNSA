namespace enrollments_microservice.Application.Dtos;

public class UserExternalDto
{
    public string? FullName { get; set; } = string.Empty;
    public List<string>? Courses { get; set; } = new List<string>();
    public int AcademicPerformance { get; set; }
    public int Credits { get; set; }
}