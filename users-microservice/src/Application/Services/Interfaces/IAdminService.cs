using users_microservice.Application.Dtos;
using static users_microservice.Application.Dtos.ServiceResponses;

namespace users_microservice.Application.Service.Interface
{
    public interface IAdminService
    {
        // Métodos para manejar administradores
        Task<GeneralResponse> CreateAdminAccountAsync(AdminDto adminDto);
        Task<AdminDto> RetrieveAdminAccountAsync(string id);
        Task<GeneralResponse> UpdateAdminAccountAsync(AdminDto adminDto);
        Task<GeneralResponse> DeleteAdminAccountAsync(string id);
        
        // Métodos para manejar estudiantes
        Task<GeneralResponse> CreateStudentAsync(StudentDto studentDto);
        Task<GeneralResponse> UpdateStudentAsync(string id, StudentDto studentDto);
        Task<GeneralResponse> DeleteStudentAsync(string id);
        Task<StudentDto?> GetStudentAsync(string id);
        Task<StudentDto> GetCoursesByStudentIdAsync(string id);
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync(); // Agregado para GetAllStudents
        Task<IEnumerable<AdminDto>> GetAllAdminsAsync();
    }
}
