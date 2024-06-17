using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public class AdminRepository : IAdminRepository
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IHttpContextAccessor httpContextAccessor;

    // Constructor
    public AdminRepository(UserManager<ApplicationUser> userManager, 
                          RoleManager<IdentityRole> roleManager, 
                          IHttpContextAccessor httpContextAccessor)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;        
        this.httpContextAccessor = httpContextAccessor;
    }

    public  async Task<GeneralResponse> CreateAdminAccount(UserDto userDto)
    {
        if (userDto is null) return new GeneralResponse(false, "Model is empty", 400);
        var newUser = new ApplicationUser()
        {
            FullName = userDto.Name,
            Email = userDto.Email,
            UserName = userDto.Email?.Split('@')[0]
        };

        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user is not null) return new GeneralResponse(false, "User registered already",409);

        var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
        if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again", 403);

        var adminRoleExists = await roleManager.RoleExistsAsync(Role.ADMIN.ToString());
        if (!adminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = Role.ADMIN.ToString() });
        }

        await userManager.AddToRoleAsync(newUser, Role.ADMIN.ToString());

        return new GeneralResponse(true, "Account Created with Admin role",200);
    }

    // Refactoring
    public async Task<ApplicationUser> GetUserById(string id)
    {
        var res = await userManager.FindByIdAsync(id) ?? throw new InvalidOperationException("No user found in DB");

        var newUser = new ApplicationUser()
        {
            Id = res.Id,
            FullName = res.FullName, // Asignar el nombre o cualquier otro campo que desees utilizar
            Email = res.Email,
            UserName = res.UserName
        };
        return newUser;
    }

    public async Task<ApplicationUser> GetUserByEmail(string email)
    {
        var res = await userManager.FindByEmailAsync(email) ?? throw new InvalidOperationException("No user found with the specified email");
        var newUser = new ApplicationUser()
        {
            Id = res.Id,
            FullName = res.FullName, // Asignar el nombre o cualquier otro campo que desees utilizar
            Email = res.Email,
            UserName = res.UserName
        };

        return newUser;
    }

    public async Task<ApplicationUser> GetUserByUserName(string userName)
    {
        var res = await userManager.FindByNameAsync(userName);
        if (res == null)
        {
            throw new InvalidOperationException("No user found with the specified username");
        }

        var newUser = new ApplicationUser()
        {
            Id = res.Id,
            FullName = res.FullName, // Asignar el nombre o cualquier otro campo que desees utilizar
            Email = res.Email,
            UserName = res.UserName
        };

        return newUser;
    }

    public async Task<GeneralResponse> UpdateUserById(UserDto userDto)
    {
        if (userDto == null || string.IsNullOrEmpty(userDto.Id))
        {
            return new GeneralResponse(false, "Invalid user ID", 400);
        }

        // Check if the user exists in the User table
        var user = await userManager.FindByIdAsync(userDto.Id);
        if (user == null)
        {
            return new GeneralResponse(false, "User not found", 404);
        }

        // Update common properties for both User and Student
        user.FullName = userDto.Name;
        user.Email = userDto.Email;

        IdentityResult result;

        if (await userManager.IsInRoleAsync(user, Role.ADMIN.ToString()))
        {
            // Update user properties in the User table
            result = await userManager.UpdateAsync(user);
        }
        else if (await userManager.IsInRoleAsync(user, Role.STUDENT.ToString()))
        {
            // Update user properties in the ApplicationUser table (AspNetUsers)
            result = await userManager.UpdateAsync(user);
        }
        else
        {
            return new GeneralResponse(false, "User is not an admin or student", 400);
        }

        // Check the update result
        if (result.Succeeded)
        {
            return new GeneralResponse(true, "User updated successfully", 200);
        }
        else
        {
            return new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 400);
        }
    }

    public async Task<GeneralResponse> DeleteUserById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return new GeneralResponse(false, "Invalid user ID", 400);
        }

        // Buscar el usuario por Id en ASP.NET Identity
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return new GeneralResponse(false, "User not found", 404);
        }

        // Eliminar el usuario de ASP.NET Identity
        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return new GeneralResponse(false, "Failed to delete user", 500); // Cambia el código de estado según sea necesario
        }

        return new GeneralResponse(true, "User deleted successfully", 200);
    }

    
}