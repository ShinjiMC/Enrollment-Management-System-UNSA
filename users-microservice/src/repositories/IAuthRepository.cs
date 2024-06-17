using users_microservice.context;
using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public interface IAuthRepository
{
    
    Task<LoginResponse> LoginAccount(LoginDto loginDto);
    
    
    // Future implementations
    // Task<ApplicationUser> GetUserById(string id);
    // Task<ApplicationUser> GetUserByEmail(string email);
    // Task<ApplicationUser> GetUserByUserName(string userName);
    // Task<GeneralResponse> UpdateUserById(UserDto userDto);
    // Task<GeneralResponse> DeleteUserById(string id);
    Task<GeneralResponse> LogOutUser(string id);
}