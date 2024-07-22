using Microsoft.EntityFrameworkCore;
using users_microservice.Domain.Repository;
using static users_microservice.Application.Dtos.ServiceResponses;
using users_microservice.Domain.Entities;
using users_microservice.Repository.Data;
using users_microservice.Domain.ValueObjects;

namespace users_microservice.Repository.Mysql
{
    public class CourseRepositoryImpl : ICourseRepository
    {
        private readonly MySqlIdentityContext _context;

        public CourseRepositoryImpl(MySqlIdentityContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> AddCourseToStudent(CourseModel studentCourse)
        {
            try
            {
                

                // Agregar la nueva relación StudentCourse a la base de datos
                _context.Courses.Add(studentCourse);
                await _context.SaveChangesAsync();

                return new GeneralResponse(true, "Curso añadido al estudiante exitosamente", 200);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return new GeneralResponse(false, $"Error al añadir el curso al estudiante: {ex.Message}", 500);
            }
        }

        public async Task<IEnumerable<CourseInfo>> GetCoursesByStudentId(int studentId)
        {
            var courses = await _context.Courses
                .Where(c => c.StudentId == studentId)
                .Select(c => c.CourseData)
                .ToListAsync();

            return courses.Where(course => course != null)!;
        }

         public async Task<GeneralResponse> RemoveCourseFromStudent(int studentId, string courseId)
        {
            // Buscar el curso por courseId y StudentId
            var courseToRemove = await _context.Courses
                .Where(c => c.StudentId == studentId && c.CourseData != null && c.CourseData.CourseId == courseId)
                .SingleOrDefaultAsync();

            if (courseToRemove == null)
            {
                return new GeneralResponse(false, "Curso no encontrado o no está asociado al estudiante", 404);
            }

            // Eliminar el curso
            _context.Courses.Remove(courseToRemove);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Curso eliminado del estudiante exitosamente", 200);
        }
    }
}
