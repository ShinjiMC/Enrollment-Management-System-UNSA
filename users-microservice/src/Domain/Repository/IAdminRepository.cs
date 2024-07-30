using users_microservice.Domain.Entities;
using static users_microservice.Application.Dtos.ServiceResponses;


namespace users_microservice.Domain.Repository
{
    public interface IAdminRepository
    {
        // Métodos para manejar administradores
        Task<GeneralResponse> CreateUser(UserModel user);
        Task<AdminModel?> GetUserById(int id);
        Task<AdminModel?> GetUserByEmail(string email);
        Task<GeneralResponse> UpdateUser(UserModel userDto);
        Task<GeneralResponse> DeleteUser(UserModel user);
        Task<List<AdminModel>> GetAllAdmins();

        // Métodos para manejar estudiantes
        Task<GeneralResponse> CreateStudent(StudentModel student);
        Task<StudentModel?> GetStudentById(int id);
        Task<GeneralResponse> UpdateStudent(StudentModel student);
        Task<GeneralResponse> DeleteStudent(StudentModel student);
        Task<List<StudentModel>> GetAllStudents();
        
        // Métodos para manejar transacciones
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
