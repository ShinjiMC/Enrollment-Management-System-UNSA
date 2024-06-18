

using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using users_microservice.repositories;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.services;

public class AdminService: IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<GeneralResponse> CreateAdminAccount(UserDto userDto)
    {
        if (userDto == null)
        {
            return new GeneralResponse(false, "Model is empty", 400);
        }

        var newUser = new ApplicationUser
        {
            FullName = userDto.Name,
            Email = userDto.Email,
            UserName = userDto.Email?.Split('@')[0]
        };

        var userExistsByEmail = await _adminRepository.GetUserByEmail(newUser.Email);
        if (userExistsByEmail != null)
        {
            return new GeneralResponse(false, "User already registered with this email", 409);
        }

        var createUserResult = await _adminRepository.CreateUser(newUser, userDto.Password);
        if (!createUserResult.Flag)
        {
            return new GeneralResponse(false, createUserResult.Message, 403);
        }

        var adminRoleExists = await _adminRepository.CheckRoleExists(Role.ADMIN.ToString());
        if (!adminRoleExists)
        {
            var createRoleResult = await _adminRepository.CreateRole(Role.ADMIN.ToString());
            if (!createRoleResult.Flag)
            {
                return new GeneralResponse(false, createRoleResult.Message, 403);
            }
        }

        var addToRoleResult = await _adminRepository.AddUserToRole(newUser, Role.ADMIN.ToString());
        if (!addToRoleResult.Flag)
        {
            return new GeneralResponse(false, addToRoleResult.Message, 403);
        }

        return new GeneralResponse(true, "Account created with Admin role", 200);
    }

    public async Task<GeneralResponse> UpdateAdminAccount(UserDto userDto)
    {
        if (userDto.Id == null)
        { 
            return new GeneralResponse(false, "User not found", 404);
        }

        var user = await _adminRepository.GetUserById(userDto.Id);
        if (user == null)
        {
            return new GeneralResponse(false, "User not found", 404);
        }

        user.FullName = userDto.Name;
        user.Email = userDto.Email;

        var result = await _adminRepository.UpdateUser(user);

        return result;
    }

    public async Task<GeneralResponse> DeleteAdminAccount(string id)
    {
        
        var user = await _adminRepository.GetUserById(id);
        if (user == null)
        {
            return new GeneralResponse(false, "User not found", 404);
        }

        var result = await _adminRepository.DeleteUser(user);

        return result;
    }
}