using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using users_microservice.context;
using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration config;

    // Constructor
    public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.config = config;
    }
    
    // Methods
    public async Task<GeneralResponse> CreateAccount(UserDto userDto)
    {
        if (userDto is null) return new GeneralResponse(false, "Model is empty");
        var newUser = new ApplicationUser()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            PasswordHash = userDto.Password,
            UserName = userDto.Email
        };
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user is not null) return new GeneralResponse(false, "User registered already");

        var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
        if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

        var usersCount = userManager.Users.Count();
        if (usersCount == 1) // Primer usuario registrado
        {
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            }

            await userManager.AddToRoleAsync(newUser, "Admin");
            return new GeneralResponse(true, "Account Created with Admin role");
        }
        else
        {
            var roleToAssign = string.IsNullOrEmpty(userDto.Role) ? "Student" : userDto.Role;
            var roleExists = await roleManager.RoleExistsAsync(roleToAssign);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = roleToAssign });
            }

            await userManager.AddToRoleAsync(newUser, roleToAssign);
            return new GeneralResponse(true, "Account Created");
        }

        // var roleExists = await roleManager.RoleExistsAsync(userDto.Role);
        // if (!roleExists)
        // {
        //     await roleManager.CreateAsync(new IdentityRole() { Name = userDto.Role });
        // }

        // await userManager.AddToRoleAsync(newUser, userDto.Role);
        //     return new GeneralResponse(true, "Account Created");

        //Assign Default Role : Admin to first registrar; rest is user
        // var checkAdmin = await roleManager.FindByNameAsync("Admin");
        // if (checkAdmin is null)
        // {
        //     await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
        //     await userManager.AddToRoleAsync(newUser, "Admin");
        //     return new GeneralResponse(true, "Account Created");
        // }
        // else
        // {
        //     var checkUser = await roleManager.FindByNameAsync("User");
        //     if (checkUser is null)
        //         await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

        //     await userManager.AddToRoleAsync(newUser, "User");
        //     return new GeneralResponse(true, "Account Created");
        // }
    }

    public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
    {
        if (loginDto == null)
            return new LoginResponse(false, null!, "Login container is empty");

        var getUser = await userManager.FindByEmailAsync(loginDto.Email);
        if (getUser is null)
            return new LoginResponse(false, null!, "User not found");

        bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDto.Password);
        if (!checkUserPasswords)
            return new LoginResponse(false, null!, "Invalid email/password");

        var getUserRole = await userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole[0]);

        string token = GenerateToken(userSession);
        return new LoginResponse(true, token!, "Login completed");
    }

    private string GenerateToken(UserSession user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id ?? ""),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, user.Role ?? "")
            };
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
