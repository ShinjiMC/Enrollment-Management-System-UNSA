using System.Net.NetworkInformation;
using users_microservice.context;
using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;
using users_microservice.models;

namespace users_microservice.repositories;

public interface ICourseRepository
{
    Task<GeneralResponse> AddCourseToStudent(StudentCourse studentCourse);
    // Task<IEnumerable<StudentCourse>> GetAllStudentCourses();
    
    // Task<StudentCourse> UpdateStudentCourse(string studentId, string courseId);
    // Task<bool> DeleteStudentCourse(int id);
    
}

