using users_microservice.Application.Dtos;
using static users_microservice.Application.Dtos.ServiceResponses;


namespace users_microservice.Application.Service.Interface
{ 
    public interface IStudentService
    {
        Task<StudentDto> GetStudentAsync(string id);
        Task<StudentDto> GetCoursesByStudentIdAsync(string studentId);
        Task<GeneralResponse> CreateStudentAccount(StudentDto studentDto);
        Task<IEnumerable<StudentDto>> GetAllStudents();
        Task<GeneralResponse> UpdateStudentById(StudentDto studentDto);
        Task<GeneralResponse> DeleteStudentById(string id);
    }
}


