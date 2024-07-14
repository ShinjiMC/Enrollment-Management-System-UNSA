namespace users_microservice.models;

public class StudentCourse
{
    public int Id { get; set; }
    public required string StudentId { get; set; }
    public required string CourseId { get; set; }
}
