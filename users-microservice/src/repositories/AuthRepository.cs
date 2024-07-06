using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using users_microservice.DTOs;
using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public class AuthRepository : IAuthRepository
{
    const string UserNotFoundErrorMessage = "User not found";
    private readonly UserManager<UserModel> _userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration config;

    // Constructor
    public AuthRepository(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        this._userManager = userManager;
        this.roleManager = roleManager;
        this.config = config;
    }
    
    // Methods

    public async Task<IdentityResult> RegisterAdmin(RegisterAdminDto registerAdminDto)
{
    var adminUser = new AdminUser()
    {
        UserName = registerAdminDto.UserName,
        Email = registerAdminDto.Email,
        FullName = registerAdminDto.FullName,
        School = registerAdminDto.School,
        PhoneNumber = registerAdminDto.PhoneNumber,
        Role = "Admin" // Set the role explicitly for AdminUser
    };

    var userExists = await _userManager.FindByEmailAsync(registerAdminDto.Email);
    if (userExists != null)
    {
        return IdentityResult.Failed(new IdentityError { Description = "User already exists" });
    }

    var result = await _userManager.CreateAsync(adminUser, registerAdminDto.Password);
    if (!result.Succeeded)
    {
        return result;
    }

    await EnsureRoleExistsAsync("Admin"); // Ensure the 'Admin' role exists
    await _userManager.AddToRoleAsync(adminUser, "Admin");

    return IdentityResult.Success;
}

public async Task<IdentityResult> RegisterStudent(RegisterStudentDto registerStudentDto)
{
    var studentUser = new StudentUser()
    {
        UserName = registerStudentDto.UserName,
        Email = registerStudentDto.Email,
        FullName = registerStudentDto.FullName,
        School = registerStudentDto.School,
        CUI = registerStudentDto.CUI,
        Courses = registerStudentDto.Courses,
        Role = "Student" // Set the role explicitly for StudentUser
    };

    var userExists = await _userManager.FindByEmailAsync(registerStudentDto.Email);
    if (userExists != null)
    {
        return IdentityResult.Failed(new IdentityError { Description = "User already exists" });
    }

    var result = await _userManager.CreateAsync(studentUser, registerStudentDto.Password);
    if (!result.Succeeded)
    {
        return result;
    }

    await EnsureRoleExistsAsync("Student"); // Ensure the 'Student' role exists
    await _userManager.AddToRoleAsync(studentUser, "Student");

    return IdentityResult.Success;
}

private async Task EnsureRoleExistsAsync(string roleName)
{
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}


    public async Task<UserModel?> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
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
    

    public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
    {

        if (loginDto == null)
            return new LoginResponse(false, null!, "Login container is empty");

        var getUser = await _userManager.FindByEmailAsync(loginDto.Email);
        if (getUser is null)
            return new LoginResponse(false, null!, UserNotFoundErrorMessage);

        bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDto.Password);
        if (!checkUserPasswords)
            return new LoginResponse(false, null!, "Invalid email/password");

        var getUserRole = await _userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, getUserRole[0]);
        string token = GenerateToken(userSession);
        return new LoginResponse(true, token!, "Login completed");
    }
}
