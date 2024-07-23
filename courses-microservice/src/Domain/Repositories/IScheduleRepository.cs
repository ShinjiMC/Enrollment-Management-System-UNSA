using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Courses;

namespace Domain.Schedules
{
    public interface IScheduleRepository
    {
        // Crear un nuevo horario
        void Add(Schedule schedule);

        // Obtener un horario por su identificador
        Task<Schedule?> GetByIdAsync(Guid id);

        // Obtener todos los horarios
        Task<List<Schedule>> GetAllAsync();

        // Actualizar un horario existente
        void Update(Schedule schedule);

        // Eliminar un horario por su identificador
        void Delete(Schedule schedule);

        // Obtener horarios por ID del curso
        Task<IReadOnlyList<Schedule>> GetByCourseIdAsync(Guid courseId);

        // Obtener horarios por ID del profesor
        Task<IReadOnlyList<Schedule>> GetByProfessorIdAsync(string professorId);

        // Obtener horarios por ID de la escuela
        Task<IReadOnlyList<Schedule>> GetBySchoolIdAsync(string schoolId);

        // Buscar horarios por año
        Task<IReadOnlyList<Schedule>> GetByYearAsync(int year);

        Task<List<Schedule>> GetBySemesterYearAndSchoolAsync(int year, string semester,  string schoolId);
    }
}
