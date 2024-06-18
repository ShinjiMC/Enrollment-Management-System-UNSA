using users_microservice.context;
using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.services;

public interface IAuthService
{
    
    Task<LoginResponse> LoginAccount(LoginDto loginDto);
    Task<GeneralResponse> LogOutUser(string id);
    public bool ValidateToken(string token);
}