namespace users_microservice.models;

public class StudentModel
{
    public int ID { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string FullName { get; set; }
    public Role Role { get; set; }
    public required string CUI { get; set; }
    public required string Email { get; set; }
    public ICollection<CourseModel> Courses { get; set; } = [];
}
