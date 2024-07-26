using users_microservice.Domain.ValueObjects;

namespace users_microservice.Domain.Entities;

public class StudentModel: UserModel
{
    
    public StudentInfo? StudentData { get; set; }
    public int? Credit { get; set; }
    public AcademicPerformance? AcademicPerformance { get; set; }
    public List<CourseModel>? StudentCourses{ get; set; }
    // Make the constructor public
    public StudentModel() { }
}
