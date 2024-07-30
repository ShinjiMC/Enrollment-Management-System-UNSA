namespace enrollments_microservice.Application.Dtos;

public class ScheduleExternalDto
{
    public string? Day { get; set; } = string.Empty;
    public string? Group { get; set; } = string.Empty;
    public int? Year { get; set; } = 0;
    public string? TeacherName { get; set; } = string.Empty;
    public List<IntervalExternalDto>? Time { get; set; } = [];
}