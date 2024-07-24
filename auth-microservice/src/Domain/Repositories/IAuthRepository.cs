
using auth_microservice.Application.DTOs;
using static auth_microservice.Application.DTOs.ServiceResponses;

public interface IAuthRepository
{
    Task<GeneralResponse> CreateAccount(UserDto userDto);
    Task<LoginResponse> LoginAccount(LoginDto loginDto);
    
    // Future implementations
    // Task<ApplicationUser> GetUserById(string id);
    // Task<ApplicationUser> GetUserByEmail(string email);
    // Task<ApplicationUser> GetUserByUserName(string userName);
    // Task<ApplicationUser> CreateUser(ApplicationUser user, string password);
    // Task<ApplicationUser> UpdateUser(ApplicationUser user);
    // Task<ApplicationUser> DeleteUser(string id);
    // Task<ApplicationUser> LoginUser(string email, string password);
    // Task<ApplicationUser> LogoutUser(string id);
}