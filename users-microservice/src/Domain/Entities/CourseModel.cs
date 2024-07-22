using users_microservice.Domain.ValueObjects;

namespace users_microservice.Domain.Entities;

public class CourseModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public CourseInfo? CourseData { get; set; }
    public DateTime? DateUpdated { get; set; }

    public CourseModel() { }
}
