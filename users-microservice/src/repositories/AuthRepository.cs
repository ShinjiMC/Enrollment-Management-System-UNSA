using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        var res = await userManager.FindByIdAsync(id) ?? throw new InvalidOperationException("No user found in DB");

        var newUser = new ApplicationUser()
        {
            Name = res.Name,
            Email = res.Email,
            UserName = res.Email
        };
        return newUser;
    }

    public async Task<ApplicationUser> GetUserByEmail(string email)
    {
        var res = await userManager.FindByEmailAsync(email) ?? throw new InvalidOperationException("No user found with the specified email");
        var newUser = new ApplicationUser()
        {
            Id = res.Id,
            Name = res.UserName, // Asignar el nombre o cualquier otro campo que desees utilizar
            Email = res.Email,
            UserName = res.Email
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
            Name = res.UserName, // Asignar el nombre o cualquier otro campo que desees utilizar
            Email = res.Email,
            UserName = res.Email
        };

        return newUser;
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
        user.Name = userDto.Name;
        user.Email = userDto.Email;

        IdentityResult result;

        if (await userManager.IsInRoleAsync(user, Role.ADMIN.ToString()))
        {
            // Update user properties in the User table
            result = await userManager.UpdateAsync(user);
        }
        else if (await userManager.IsInRoleAsync(user, Role.STUDENT.ToString()))
        {
            // Update user properties in the StudentModel table
            var student = await context.StudentsModel.FirstOrDefaultAsync(s => s.Email == userDto.Email);
            if (student == null)
            {
                return new GeneralResponse(false, "Student not found", 404);
            }

            // Update properties in StudentModel
            student.UserName = userDto.Email;
            student.FullName = userDto.Name;
            student.Email = userDto.Email;

            // Save changes to StudentModel table
            context.StudentsModel.Update(student);
            await context.SaveChangesAsync();

            // Update properties in ApplicationUser table
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

   

    public Task<GeneralResponse> LogOutUser(string id)
    {
        // Implement logout logic here (e.g., removing user sessions, tokens, etc.)
        // localStorage.removeItem('token'); // Eliminar el token del almacenamiento local
        // // Redirigir a la página de inicio de sesión u otra página deseada
        // window.location.href = '/login';
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

        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user is not null) return new GeneralResponse(false, "User registered already",409);

        var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
        if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again", 403);

        var usersCount = userManager.Users.Count();
        if (usersCount == 1) // Primer usuario registrado
        {
            var adminRoleExists = await roleManager.RoleExistsAsync(Role.ADMIN.ToString());
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = Role.ADMIN.ToString() });
            }

            await userManager.AddToRoleAsync(newUser, "Admin");
            return new GeneralResponse(true, "Account Created with Admin role",200);
        }
        else
        {
            var roleToAssign = string.IsNullOrEmpty(userDto.Role) ? Role.STUDENT.ToString() : userDto.Role;
            var roleExists = await roleManager.RoleExistsAsync(roleToAssign);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = roleToAssign });
            }

            await userManager.AddToRoleAsync(newUser, roleToAssign);
           return new GeneralResponse(true, $"Account created with  role", 201);
        }
    }

    public async Task<GeneralResponse> CreateAccountStudent(StudentDto studentDto)
    {
        var roleToAssign = Role.STUDENT;

        // Obtener el claim de rol del contexto
        var userRoleClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (userRoleClaim == null || !userRoleClaim.Value.Equals(Role.ADMIN.ToString(), StringComparison.OrdinalIgnoreCase))
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
        var user = await userManager.FindByEmailAsync(newStudentUser.Email);
        if (user is not null) return new GeneralResponse(false, "User registered already",409);
        
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
            return new LoginResponse(false, "--", "Login container is empty", 400);

        var getUser = await userManager.FindByEmailAsync(loginDto.Email);
        if (getUser is null)
            return new LoginResponse(false, "--", "User not found", 401);

        bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDto.Password);
        if (!checkUserPasswords)
            return new LoginResponse(false, "--", "Invalid email/password", 401);

        var getUserRole = await userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole[0]);

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
