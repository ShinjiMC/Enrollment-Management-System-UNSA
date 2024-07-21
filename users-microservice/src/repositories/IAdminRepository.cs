using System.Net.NetworkInformation;
using users_microservice.context;
using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public interface IAdminRepository
{
    Task<GeneralResponse> CreateUser(ApplicationUser user, string password);
    Task<ApplicationUser?> GetUserById(string id);
    Task<ApplicationUser?> GetUserByEmail(string email);
    Task<ApplicationUser?> GetUserByUserName(string userName);
    Task<GeneralResponse> UpdateUser(ApplicationUser userDto);
    Task<GeneralResponse> DeleteUser(ApplicationUser user);
    Task<bool> CheckRoleExists(string roleName);
    Task<GeneralResponse> CreateRole(string roleName);
    Task<GeneralResponse> AddUserToRole(ApplicationUser user, string roleName);
    Task<IList<ApplicationUser>> GetAllUserByRol(string userRole);
    Task<bool> CheckPassword(ApplicationUser user, string password);
    Task<IList<string>> GetUserRole(ApplicationUser user);
}

