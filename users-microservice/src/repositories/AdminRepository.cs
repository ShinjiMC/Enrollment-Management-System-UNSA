using System.Net.NetworkInformation;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public class AdminRepository : IAdminRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<GeneralResponse> CreateUser(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded
            ? new GeneralResponse(true, "User created successfully", 200)
            : new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 403);
    }

    public async Task<ApplicationUser?> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<ApplicationUser?> GetUserByUserName(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user;
    }

    public async Task<GeneralResponse> UpdateUser(ApplicationUser userDto)
    {
        var updateUserResult = await  _userManager.UpdateAsync(userDto);
        
        return updateUserResult.Succeeded
            ? new GeneralResponse(true, "User updated successfully", 200)
            : new GeneralResponse(false, string.Join(", ", updateUserResult.Errors.Select(e => e.Description)), 400);
    }

    public async Task<GeneralResponse> DeleteUser(ApplicationUser user)
    {
        var deleteUserResult = await _userManager.DeleteAsync(user);

        return deleteUserResult.Succeeded
            ? new GeneralResponse(true, "Admin account deleted successfully", 200)
            : new GeneralResponse(false, string.Join(", ", deleteUserResult.Errors.Select(e => e.Description)), 500);

    }
    public async Task<bool> CheckRoleExists(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<GeneralResponse> CreateRole(string roleName)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

        return result.Succeeded
            ? new GeneralResponse(true, "Role created successfully", 200)
            : new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 403);
    }

    public async Task<GeneralResponse> AddUserToRole(ApplicationUser user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName);

        return result.Succeeded
            ? new GeneralResponse(true, "User added to role successfully", 200)
            : new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 403);
    }
    
    public async Task<IList<ApplicationUser>> GetAllUserByRol(string userRole){
        
        var listUsers = await _userManager.GetUsersInRoleAsync(userRole);
        
        return listUsers?.ToList() ?? [];
    }


    // Implementación de los nuevos métodos
    public async Task<bool> CheckPassword(ApplicationUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IList<string>> GetUserRole(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}