using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using users_microservice.repositories;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.services;

public class StudentService : IStudentService
{
        private readonly IAdminRepository _adminRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

    public StudentService(IAdminRepository adminRepository, IHttpContextAccessor httpContextAccessor)
    {
        _adminRepository = adminRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GeneralResponse> CreateStudentAccount(StudentDto studentDto)
    {
        if (studentDto == null)
        {
            return new GeneralResponse(false, "Model is empty", 400);
        }

        var newStudentUser = new Student
        {
            FullName = studentDto.FullName,
            Email = studentDto.Email,
            UserName = studentDto.Email?.Split('@')[0],
            Cui = studentDto.CUI
        };


        var userExistsByEmail = await _adminRepository.GetUserByEmail(newStudentUser.Email);
        if (userExistsByEmail != null)
        {
            return new GeneralResponse(false, "User already registered with this email", 409);
        }

        
        var createUserResult = await _adminRepository.CreateUser(newStudentUser, studentDto.Password);
        if (!createUserResult.Flag)
        {
            return createUserResult;
        }

        var roleToAssign = Role.STUDENT;

        if (!await _adminRepository.CheckRoleExists(roleToAssign.ToString()))
        {
            await _adminRepository.CreateRole(roleToAssign.ToString());
        }

        var addToRoleResult = await _adminRepository.AddUserToRole(newStudentUser, roleToAssign.ToString());
        if (!addToRoleResult.Flag)
        {
            return addToRoleResult;
        }

        return new GeneralResponse(true, "Account created", 201);
    }

    public async Task<List<StudentDto>> GetAllStudents()
    {
        var students = await _adminRepository.GetAllUserByRol(Role.STUDENT.ToString());
        if (students.IsNullOrEmpty())
        {
            return [];
        }


        var studentDtos = students.Select(user => new StudentDto
        {
            Id = user.Id ?? string.Empty, // Usar string.Empty si Id es null
            FullName = user.FullName ?? "Unknown", // Asignar un valor predeterminado si FullName es null
            Email = user.Email ?? "NoEmail@domain.com", // Asignar un valor predeterminado si Email es null
            UserName = user.UserName ?? "UnknownUser", // Asignar un valor predeterminado si UserName es null
            CUI = (user as Student)?.Cui ?? "UnknownCUI" // Asignar un valor predeterminado si CUI es null
            // Agregar otros campos específicos de StudentDto si es necesario
        }).ToList();

        // Devolver una lista vacía si students es null
        return studentDtos;
    }

    public async Task<GeneralResponse> UpdateStudentById(StudentDto studentDto)
    {
        var userRoleClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (userRoleClaim == null || !userRoleClaim.Value.Equals(Role.ADMIN.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            return new GeneralResponse(false, "Unauthorized. Only Admin can update students.", 401);
        }

        if (studentDto == null || string.IsNullOrEmpty(studentDto.Id))
        {
            return new GeneralResponse(false, "Invalid student ID", 400);
        }

        var student = await _adminRepository.GetUserById(studentDto.Id);
        if (student == null)
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        student.FullName = studentDto.FullName;
        student.Email = studentDto.Email;
        

        var result = await _adminRepository.UpdateUser(student);

        return result;
        
    }

    public async Task<GeneralResponse> DeleteStudentById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return new GeneralResponse(false, "Invalid student ID", 400);
        }

        var student = await _adminRepository.GetUserById(id);
        if (student == null)
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        var result = await _adminRepository.DeleteUser(student);
        return result;
    }
}