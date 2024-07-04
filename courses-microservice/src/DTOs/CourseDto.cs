namespace course_microservice.models;

public class CourseDto
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public char Semester { get; set; }
    public int Credits { get; set; }
    public int Year { get; set; }
}
