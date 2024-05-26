namespace users_microservice.models
{
    public enum Role
    {
        STUDENT,
        ADMIN
    }
    public class UserModel
    {
        public int ID { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public Role Role { get; set; }
    }

    public class Student : UserModel
    {
        public string? CUI { get; set; }
        public string? Email { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    public class Admin : UserModel
    {
        public string? Department { get; set; }
    }
}