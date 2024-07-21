using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using users_microservice.DTOs;
using users_microservice.repositories;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.services;

public class AuthService : IAuthService
{
    private readonly IConfiguration config;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IAdminRepository _adminRepository;

   
    private static readonly ConcurrentDictionary<string, bool> revokedTokens = new ConcurrentDictionary<string, bool>();

    // Constructor
    // Constructor
    public AuthService(
                          IConfiguration config,
                          IHttpContextAccessor httpContextAccessor,
                          IAdminRepository adminRepository)
    {
        
        this.config = config;
        this.httpContextAccessor = httpContextAccessor;
        this._adminRepository = adminRepository;
    }

    // Implementation of new methods    

    public async Task<GeneralResponse> LogOutUser(string id)
    {
        HttpContext httpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is not available.");

        await httpContext.SignOutAsync();

        var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            revokedTokens[token] = true;
        }

        httpContext.Session.Clear();

        return new GeneralResponse(true, "Logout successful", 200);
    }

    // Existing methods

    public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
    {
        if (loginDto == null)
            return new LoginResponse(false, "--", "Login container is empty", 400);

        var getUser = await _adminRepository.GetUserByEmail(loginDto.Email);
        if (getUser is null)
            return new LoginResponse(false, "--", "User not found", 401);

        bool checkUserPasswords = await _adminRepository.CheckPassword(getUser, loginDto.Password);
        if (!checkUserPasswords)
            return new LoginResponse(false, "--", "Invalid email/password", 401);

        var getUserRole = await _adminRepository.GetUserRole(getUser);
        
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
    public bool ValidateToken(string token)
    {
        if (revokedTokens.ContainsKey(token))
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
