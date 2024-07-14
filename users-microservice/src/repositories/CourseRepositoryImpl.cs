using System.Net.NetworkInformation;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using users_microservice.context;
using users_microservice.DTOs;
using users_microservice.models;
using static users_microservice.DTOs.ServiceResponses;
using Microsoft.EntityFrameworkCore;


namespace users_microservice.repositories;

public class CourseRepositoryImpl : ICourseRepository
{
    private readonly MySqlIdentityContext _context;

    public CourseRepositoryImpl(MySqlIdentityContext context)
    {
        _context = context;
    }

    public async Task<GeneralResponse> AddCourseToStudent(StudentCourse studentCourse)
    {
        try
        {
            // Verificar si ya existe una relación StudentCourse con estos IDs
            var existingRelation = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == studentCourse.StudentId && sc.CourseId == studentCourse.CourseId);

            if (existingRelation != null)
            {
                // Si la relación ya existe, puedes manejarlo como desees, por ejemplo, lanzar una excepción o devolver un mensaje de error.
                return new GeneralResponse(false, "El curso ya está asignado al estudiante.", 403);
            }

            // Agregar la nueva relación StudentCourse a la base de datos
            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Curso añadido al estudiante exitosamente", 200);
        }
        catch (Exception ex)
        {
            // Manejo de errores
            return new GeneralResponse(false, $"Error al añadir el curso al estudiante: {ex.Message}", 500);
        }
    }
    // public async Task<IEnumerable<StudentCourse>> GetAllStudentCourses();
    
    // public async Task<StudentCourse> UpdateStudentCourse(string studentId, string courseId);
    // public async Task<bool> DeleteStudentCourse(int id);
}