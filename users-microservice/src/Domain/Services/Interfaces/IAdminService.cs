using users_microservice.Application.Dtos;
using users_microservice.Domain.Entities;
using static users_microservice.Application.Dtos.ServiceResponses;


namespace users_microservice.Domain.Services.Interfaces;

public interface IAdminServiceDomain
{
    
   // Métodos para manejar administradores
        Task<GeneralResponse> CreateAdminAccount(AdminModel userDto);
        Task<GeneralResponse> UpdateAdminAccount(AdminModel userDto);
        Task<GeneralResponse> DeleteAdminAccount(int id);
        Task<AdminModel> GetAdminById(int id);
        
        // Métodos para manejar estudiantes
        Task<GeneralResponse> CreateStudent(StudentModel studentModel);
        Task<GeneralResponse> CreateStudentCourse(List<CourseModel> courseModel);
        Task<GeneralResponse> UpdateStudent(StudentModel studentModel);
        Task<GeneralResponse> DeleteStudent(int id);
        Task<StudentModel?> GetStudent(int id);
        Task<StudentModel> GetCoursesByStudentId(int id);
}