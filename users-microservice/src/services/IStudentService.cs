
using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.services;

public interface IStudentService 
{   
    Task<GeneralResponse> CreateStudentAccount(StudentDto studentDto);
    Task<List<StudentDto>> GetAllStudents();
    Task<GeneralResponse> UpdateStudentById(StudentDto studentDto);
    Task<GeneralResponse> DeleteStudentById(string id);
}
