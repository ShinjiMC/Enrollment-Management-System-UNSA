using users_microservice.Domain.Entities;
using static users_microservice.Application.Dtos.ServiceResponses;

namespace users_microservice.Domain.Services.Interfaces;

public interface IStudentServiceDomain 
{   
    Task<GeneralResponse> CreateStudent(StudentModel studentModel);
    Task<GeneralResponse> CreateStudentCourse(List<CourseModel> courseModel);
    
    Task<GeneralResponse> UpdateStudent(StudentModel studentModel);
    Task<GeneralResponse> DeleteStudent(int id);
    Task<StudentModel?> GetStudent(int id);
    Task<StudentModel> GetCoursesByStudentId(int id);
}
