namespace enrollments_microservice.Application.Dtos;

public class SchoolExternalDto
{
    public string? FullName { get; set; } = string.Empty;
    public List<PreCoursesExternalDto>? Curriculum { get; set; } = new List<PreCoursesExternalDto>();
}