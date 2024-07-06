using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using users_microservice.models;
namespace users_microservice.repositories;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterAdmin(RegisterAdminDto registerAdminDto);
    Task<IdentityResult> RegisterStudent(RegisterStudentDto registerStudentDto);
    Task<UserModel?> FindByEmailAsync(string email);
    Task<LoginResponse> LoginAccount(LoginDto loginDto);
}