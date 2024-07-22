namespace enrollments_microservice.Application.Dtos;

public class ScheduleDto
{
    public string Day { get; set; }
    public string Group { get; set; }
    public int Year { get; set; }
    public string TeacherName { get; set; }
    public List<IntervalDto> Time { get; set; }
}