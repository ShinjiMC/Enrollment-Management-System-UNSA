namespace enrollments_microservice.Application.Dtos;

public class CourseDto
{
    public int CourseID { get; set; }
    public string Name { get; set; }
    public string Semester { get; set; }
    public int Credits { get; set; }
    public List<ScheduleDto> Schedules { get; set; }
}