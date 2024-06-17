using Microsoft.EntityFrameworkCore;
using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;
using Microsoft.AspNetCore.Identity;

namespace users_microservice.repositories;


public class StudentRepository : IStudentRepository
{

    // public StudentRepository()
    // {
    //     // Inicialización de datos de ejemplo
    //     _students.Add(new () { 
    //         ID = 1, 
    //         UserName = "student1", 
    //         Password = "pass1", 
    //         FullName = "Student One", 
    //         Role = Role.STUDENT, 
    //         CUI = "123456789", 
    //         Email = "student1@example.com" });
    //     _students.Add(new () { 
    //         ID = 2, 
    //         UserName = "student2", 
    //         Password = "pass2", 
    //         FullName = "Student Two", 
    //         Role = Role.STUDENT, 
    //         CUI = "987654321", 
    //         Email = "student2@example.com" });
    // }

    private readonly MySqlIdentityContext _context;

    public StudentRepository(MySqlIdentityContext context)
    {
        _context = context;
    }

    public async Task<List<StudentModel>> GetAllStudents()
    {
        return await _context.StudentsModel.ToListAsync();
    }

    public async Task<StudentModel> GetStudentByCUI(string cui)
    {
        
        var res = await _context.StudentsModel.FirstOrDefaultAsync(s => s.CUI == cui) ?? throw new InvalidOperationException("No student in DB");
        return res;
        
    }

    public async Task<GeneralResponse> UpdateStudent(StudentDto studentDto)
    {
        if (studentDto is not null)
        {
            var existingStudent = await _context.StudentsModel.FirstOrDefaultAsync(s => s.CUI == studentDto.CUI);
            if (existingStudent == null)
            {
                return new GeneralResponse(false, "Student not found", 404);
            }

            // Actualizar la información del estudiante en la tabla Students
            existingStudent.UserName = studentDto.UserName;
            existingStudent.Password = studentDto.Password;
            existingStudent.FullName = studentDto.FullName;
            existingStudent.CUI = studentDto.CUI;
            existingStudent.Email = studentDto.Email;

            _context.StudentsModel.Update(existingStudent);
            await _context.SaveChangesAsync();

            // Buscar y actualizar la información en la tabla UserManager usando el ID del estudiante
            // var userManagerEntry = await _context.UserManager.FirstOrDefaultAsync(u => u.StudentId == existingStudent.Id);
            // if (userManagerEntry != null)
            // {
            //     userManagerEntry.UserName = studentDto.UserName;
            //     userManagerEntry.Password = studentDto.Password;

            //     _context.UserManager.Update(userManagerEntry);
            //     await _context.SaveChangesAsync();
            // }

            return new GeneralResponse(true, "Student and UserManager updated successfully", 200);
        }

        throw new ArgumentNullException(nameof(studentDto));
    }


    public async Task<GeneralResponse> DeleteStudent(string cui)
    {
        // Buscar el estudiante por su CUI
        var student = await _context.StudentsModel.FirstOrDefaultAsync(s => s.CUI == cui);
        if (student == null)
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        // Eliminar la entrada correspondiente en la tabla UserManager usando el ID del estudiante
        // var userManagerEntry = await userManager.FirstOrDefaultAsync(u => u.StudentId == student.Id);
        // if (userManagerEntry != null)
        // {
        //     userManager.Remove(userManagerEntry);
        // }

        // Eliminar el estudiante de la tabla Students
        _context.StudentsModel.Remove(student);
        await _context.SaveChangesAsync();

        return new GeneralResponse(true, "Student and related UserManager entry deleted successfully", 200);
    
    }
}