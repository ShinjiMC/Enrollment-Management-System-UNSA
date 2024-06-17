using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.DTOs;

public interface IStudentRepository
{
    Task<List<StudentModel>> GetAllStudents();
    Task<StudentModel> GetStudentByCUI(string cui);
    Task<GeneralResponse> UpdateStudent(StudentDto studentDto);
    Task<GeneralResponse> DeleteStudent(string cui);
}