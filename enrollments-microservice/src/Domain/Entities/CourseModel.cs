namespace enrollments_microservice.Domain.Entities;
public class CourseModel
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? Group { get; set; }
    public CourseModel() { }
}
