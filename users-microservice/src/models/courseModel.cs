namespace users_microservice.models;

public class CourseModel
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Semester { get; set; }
    public int Credits { get; set; }
    public string? StudentUserId { get; set; }
    public StudentUser? StudentUser { get; set; }
}
