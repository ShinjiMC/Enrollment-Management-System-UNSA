using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration config;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly MySqlIdentityContext context;

    // Constructor
    public AuthRepository(UserManager<ApplicationUser> userManager, 
                          RoleManager<IdentityRole> roleManager, 
                          IConfiguration config, 
                          IHttpContextAccessor httpContextAccessor,
                          MySqlIdentityContext context)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.config = config;
        this.httpContextAccessor = httpContextAccessor;
        this.context = context;
    }

    // Implementation of new methods
    public async Task<ApplicationUser> GetUserById(string id)
    {
        return await userManager.FindByIdAsync(id);
    }

    public async Task<ApplicationUser> GetUserByEmail(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser> GetUserByUserName(string userName)
    {
        return await userManager.FindByNameAsync(userName);
    }

    public async Task<GeneralResponse> CreateUser(ApplicationUser user, string password)
    {
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            return new GeneralResponse(true, "User created successfully", 201);
        }
        return new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 400);
    }

    public async Task<GeneralResponse> UpdateUser(ApplicationUser user)
    {
        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new GeneralResponse(true, "User updated successfully", 200);
        }
        return new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 400);
    }

    public async Task<GeneralResponse> DeleteUser(string id)
    {
        var user = await GetUserById(id);
        if (user == null)
        {
            return new GeneralResponse(false, "User not found", 404);
        }
        var result = await userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return new GeneralResponse(true, "User deleted successfully", 200);
        }
        return new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 400);
    }

    public async Task<LoginResponse> LoginUser(string email, string password)
    {
        var user = await GetUserByEmail(email);
        if (user == null)
        {
            return new LoginResponse(false, null!, "User not found", 404);
        }
        var result = await userManager.CheckPasswordAsync(user, password);
        if (!result)
        {
            return new LoginResponse(false, null!, "Invalid email/password", 401);
        }

        var roles = await userManager.GetRolesAsync(user);
        var userSession = new UserSession(user.Id, user.Name, user.Email, roles.FirstOrDefault());

        string token = GenerateToken(userSession);
        return new LoginResponse(true, token, "Login successful", 200);
    }

    public Task<GeneralResponse> LogoutUser(string id)
    {
        // Implement logout logic here (e.g., removing user sessions, tokens, etc.)
        return Task.FromResult(new GeneralResponse(true, "Logout successful", 200));
    }

    // Existing methods

    public async Task<GeneralResponse> CreateAccount(UserDto userDto)
    {
        if (userDto is null) return new GeneralResponse(false, "Model is empty", 400);

        var newUser = new ApplicationUser()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            UserName = userDto.Email
        };

        var existingUser = await GetUserByEmail(newUser.Email);
        if (existingUser != null) return new GeneralResponse(false, "User already registered", 409);

        var createUserResponse = await CreateUser(newUser, userDto.Password);
        if (!createUserResponse.Flag) return createUserResponse;

        var usersCount = userManager.Users.Count();
        var roleName = usersCount == 1 ? Role.ADMIN.ToString() : Role.STUDENT.ToString();

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        await userManager.AddToRoleAsync(newUser, roleName);

        return new GeneralResponse(true, $"Account created with {roleName} role", 201);
    }

    public async Task<GeneralResponse> CreateAccountStudent(StudentDto studentDto)
    {
        var roleToAssign = Role.STUDENT;

        // Obtener el claim de rol del contexto
        var userRoleClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (userRoleClaim == null || !userRoleClaim.Value.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
        {
            return new GeneralResponse(false, "Unauthorized. Only Admin can create students.", 401);
        }

        if (studentDto is null) return new GeneralResponse(false, "Model is empty", 400);

        var newStudentUser = new ApplicationUser()
        {
            Name = studentDto.FullName,
            Email = studentDto.Email,
            UserName = studentDto.Email
        };

        var existingUser = await GetUserByEmail(newStudentUser.Email);
        if (existingUser != null) return new GeneralResponse(false, "Student already registered", 409);

        var createUserResponse = await CreateUser(newStudentUser, studentDto.Password);
        if (!createUserResponse.Flag) return createUserResponse;

        if (!await roleManager.RoleExistsAsync(roleToAssign.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(roleToAssign.ToString()));
        }

        await userManager.AddToRoleAsync(newStudentUser, roleToAssign.ToString());

        var student = new StudentModel
        {
            UserName = studentDto.UserName,
            Password = studentDto.Password,
            FullName = studentDto.FullName,
            Role = roleToAssign,
            CUI = studentDto.CUI,
            Email = studentDto.Email
        };

        await context.StudentsModel.AddAsync(student);
        await context.SaveChangesAsync();

        return new GeneralResponse(true, "Account created", 201);
    }

    public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
    {
        if (loginDto == null)
            return new LoginResponse(false, null!, "Login container is empty", 400);

        return await LoginUser(loginDto.Email, loginDto.Password);
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
