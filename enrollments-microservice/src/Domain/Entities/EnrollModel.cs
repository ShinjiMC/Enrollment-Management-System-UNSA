using enrollments_microservice.Domain.ValueObjects;

namespace enrollments_microservice.Domain.Entities;
public class EnrollModel
{
    public int Id { get; set; }
    public StudentData? Student { get; set; }
    public List<CourseModel>? Courses { get; set; }
    public SchoolData? School { get; set; }
}