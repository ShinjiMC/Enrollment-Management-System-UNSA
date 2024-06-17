using Microsoft.EntityFrameworkCore;
using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace users_microservice.repositories;


public class StudentRepository : IStudentRepository
{

   
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IHttpContextAccessor httpContextAccessor;

    // Constructor
    public StudentRepository(UserManager<ApplicationUser> userManager, 
                          RoleManager<IdentityRole> roleManager, 
                          IHttpContextAccessor httpContextAccessor)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;        
        this.httpContextAccessor = httpContextAccessor;
    }

    public  async Task<GeneralResponse> CreateStudentAccount(StudentDto studentDto)
    {
        var roleToAssign = Role.STUDENT;

        // Obtener el claim de rol del contexto
        var userRoleClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (userRoleClaim == null || !userRoleClaim.Value.Equals(Role.ADMIN.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return new GeneralResponse(false, "Unauthorized. Only Admin can create students.", 401);
        }

        if (studentDto is null) return new GeneralResponse(false, "Model is empty", 400);

        // Crear un nuevo usuario de tipo Student
        var newStudentUser = new Student()
        {
            FullName = studentDto.FullName,
            Email = studentDto.Email,
            UserName = studentDto.Email?.Split('@')[0],
            Cui = studentDto.CUI
            // Agregar otros campos específicos de Student si es necesario
        };

        var createUserResult = await userManager.CreateAsync(newStudentUser!, studentDto.Password);
        if (!createUserResult.Succeeded)
        {
            // Devuelve el primer error encontrado para mayor claridad
            var errorMessage = createUserResult.Errors.FirstOrDefault()?.Description ?? "Error occurred. Please try again";
            return new GeneralResponse(false, errorMessage, 403);
        }

        if (!await roleManager.RoleExistsAsync(roleToAssign.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(roleToAssign.ToString()));
        }

        var addToRoleResult = await userManager.AddToRoleAsync(newStudentUser, roleToAssign.ToString());
        if (!addToRoleResult.Succeeded)
        {
            var errorMessage = addToRoleResult.Errors.FirstOrDefault()?.Description ?? "Error occurred while adding role. Please try again";
            return new GeneralResponse(false, errorMessage, 403);
        }
        return new GeneralResponse(true, "Account created", 201);
    }

    public async Task<List<StudentDto>> GetAllStudents()
    {
        var students = await userManager.GetUsersInRoleAsync(Role.STUDENT.ToString());

        var studentDtos = students.Select(user => new StudentDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            UserName = user.UserName,
            CUI = (user as Student)?.Cui, // Aquí se asume que Student es una clase derivada de ApplicationUser y tiene el campo CUI
            // Agregar otros campos específicos de StudentDto si es necesario
        }).ToList();

        return studentDtos;
    }

    public async Task<StudentDto> GetStudentByCUI(string cui)
    {
        // Obtener todos los usuarios que tienen el rol de estudiante
        var studentUsers = await userManager.GetUsersInRoleAsync(Role.STUDENT.ToString());

        // Buscar el estudiante por el campo CUI entre los usuarios con el rol de estudiante
        var studentUser = studentUsers.FirstOrDefault(u => u.Cui == cui);

        if (studentUser == null)
        {
            throw new InvalidOperationException("No student found in DB with the given CUI");
        }

        // Mapear el estudiante encontrado a StudentDto
        var studentDto = new StudentDto
        {
            Id = studentUser.Id,
            FullName = studentUser.FullName,
            Email = studentUser.Email,
            UserName = studentUser.UserName,
            CUI = studentUser.CUI,
            // Agregar otros campos específicos de StudentDto si es necesario
        };

        return studentDto;
        
    }

    public async Task<GeneralResponse> UpdateStudentById(StudentDto studentDto)
    {
         // Obtener el claim de rol del contexto
        var userRoleClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (userRoleClaim == null || !userRoleClaim.Value.Equals(Role.ADMIN.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return new GeneralResponse(false, "Unauthorized. Only Admin can create students.", 401);
        }

        if (studentDto == null || string.IsNullOrEmpty(studentDto.Id))
        {
            return new GeneralResponse(false, "Invalid student ID", 400);
        }

        // Buscar al estudiante por su Id
        var student = await userManager.FindByIdAsync(studentDto.Id);
        if (student == null)
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        // Actualizar propiedades específicas de Student
        student.FullName = studentDto.FullName;
        student.Email = studentDto.Email;
        student.Cui = studentDto.CUI;

        // Actualizar el estudiante en la tabla ApplicationUser (AspNetUsers)
        var result = await userManager.UpdateAsync(student);

        // Verificar el resultado de la actualización
        if (result.Succeeded)
        {
            return new GeneralResponse(true, "Student updated successfully", 200);
        }
        else
        {
            return new GeneralResponse(false, string.Join(", ", result.Errors.Select(e => e.Description)), 400);
        }
    }


    public async Task<GeneralResponse> DeleteStudentById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return new GeneralResponse(false, "Invalid student ID", 400);
        }

        // Buscar el estudiante por Id
        var student = await userManager.FindByIdAsync(id);
        if (student == null)
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        // Eliminar el usuario (que también es estudiante) de ASP.NET Identity
        var result = await userManager.DeleteAsync(student);
        if (!result.Succeeded)
        {
            return new GeneralResponse(false, "Failed to delete student", 500); // Cambia el código de estado según sea necesario
        }

        return new GeneralResponse(true, "Student deleted successfully", 200);
    }
}