using users_microservice.Domain.Entities;

namespace users_microservice.Application.Dtos;

public class ExtStudentCourseDto{
    
    public string FullName { get; set; } = string.Empty;
    // You can add it if necessary, for example:
    public List<string> CourseIds { get; set; } = new List<string>();

    public AcademicPerformance? AcademicPerformance { get; set; }
    public int? Credit { get; set; }
    
}