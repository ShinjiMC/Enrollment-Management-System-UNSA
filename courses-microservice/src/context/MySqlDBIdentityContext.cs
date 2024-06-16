using Microsoft.EntityFrameworkCore;
using course_microservice.models;
using course_microservice.DTOs;

namespace course_microservice.context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            // Verificar si existen cursos con los mismos IDs y eliminarlos si es necesario
            var existingCourse1 = Course.Find(1);
            if (existingCourse1 != null)
                Course.Remove(existingCourse1);

            var existingCourse2 = Course.Find(2);
            if (existingCourse2 != null)
                Course.Remove(existingCourse2);

            // Añadir dos cursos al crear la base de datos
            CourseModel course1 = new CourseModel
            {
                ID = 1,
                Name = "Curso 1",
                Semester = 'A',
                Credits = 3
            };

            CourseModel course2 = new CourseModel
            {
                ID = 2,
                Name = "Curso 2",
                Semester = 'B',
                Credits = 4
            };

            // Agregar los cursos al contexto
            Course.Add(course1);
            Course.Add(course2);

            // Guardar cambios en la base de datos
            SaveChanges();
        }

        public DbSet<CourseModel> Course { get; set; }
        public DbSet<SchoolModel> School { get; set; }
        public DbSet<WeekDayModel> WeekDay { get; set; }

    }
}
