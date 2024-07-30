namespace enrollments_microservice.Application.Dtos;

public class IntervalExternalDto
{
    public string? DayOfWeek { get; set; } = string.Empty;
    public string? Start { get; set; } = string.Empty;
    public string? End { get; set; } = string.Empty;
}