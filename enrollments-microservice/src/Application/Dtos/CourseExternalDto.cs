namespace enrollments_microservice.Application.Dtos;

public class CourseExternalDto
{
    public string? Name { get; set; } = string.Empty;
    public string? Semester { get; set; } = string.Empty;
    public int? Credits { get; set; } = 0;
    public List<ScheduleExternalDto>? Schedules { get; set; } = [];
}