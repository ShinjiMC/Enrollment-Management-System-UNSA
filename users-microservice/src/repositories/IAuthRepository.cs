using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public interface IAuthRepository
{
    Task<GeneralResponse> CreateAccount(UserDto userDto);
    Task<LoginResponse> LoginAccount(LoginDto loginDto);
    Task<GeneralResponse> CreateAccountStudent(StudentDto studentDto);
    
    // Future implementations
    // Task<GeneralResponse> GetUserById(string id);
    // Task<GeneralResponse> GetUserByEmail(string email);
    // Task<GeneralResponse> GetUserByUserName(string userName);
    // Task<GeneralResponse> CreateUser(ApplicationUser user, string password);
    // Task<GeneralResponse> UpdateUser(ApplicationUser user);
    // Task<GeneralResponse> DeleteUser(string id);
    // Task<GeneralResponse> LoginUser(string email, string password);
    // Task<GeneralResponse> LogoutUser(string id);
}