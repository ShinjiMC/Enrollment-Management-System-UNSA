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
   
    private readonly IConfiguration config;
   
    

    // Constructor
    public AuthRepository(UserManager<ApplicationUser> userManager, 
                          IConfiguration config)
    {
        this.userManager = userManager;
        this.config = config;
       
    }

    // Implementation of new methods    

    public Task<GeneralResponse> LogOutUser(string id)
    {
        // Implement logout logic here (e.g., removing user sessions, tokens, etc.)
        // localStorage.removeItem('token'); // Eliminar el token del almacenamiento local
        // // Redirigir a la página de inicio de sesión u otra página deseada
        // window.location.href = '/login';
        return Task.FromResult(new GeneralResponse(true, "Logout successful", 200));
    }

    // Existing methods

    public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
    {
        if (loginDto == null)
            return new LoginResponse(false, "--", "Login container is empty", 400);

        var getUser = await userManager.FindByEmailAsync(loginDto.Email);
        if (getUser is null)
            return new LoginResponse(false, "--", "User not found", 401);

        bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDto.Password);
        if (!checkUserPasswords)
            return new LoginResponse(false, "--", "Invalid email/password", 401);

        var getUserRole = await userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, getUserRole[0]);

        string token = GenerateToken(userSession);
        return new LoginResponse(true, token!, "Login completed",200);
    }
    private string GenerateToken(UserSession user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userClaims = new[]
        {
            new Claim("id", user.Id ?? ""),
            new Claim("username", user.Name ?? ""),
            new Claim("email", user.Email ?? ""),
            new Claim("role", user.Role ?? "")
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
