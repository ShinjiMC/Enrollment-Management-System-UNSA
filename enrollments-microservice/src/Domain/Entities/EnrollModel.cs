namespace enrollments_microservice.Domain.Entities;
using enrollments_microservice.Domain.ValueObjects;
public class EnrollModel
{
    public EnrollData Id { get; set; }
    public StudentData Student { get; set; }
    public List<CourseData> Courses { get; set; }
    public SchoolData School { get; set; }
}
