namespace enrollments_microservice.Application.Dtos;

public class PreCoursesExternalDto
{
    public string? Id { get; set; } = string.Empty;
    public List<string>? PreRequeriments { get; set; } = new List<string>();
}