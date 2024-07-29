using Domain.Schedules;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public ScheduleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Schedule schedule) => _context.Schedules.Add(schedule);

        public async Task<Schedule?> GetByIdAsync(Guid id)
        {
            return await _context.Schedules
                .Include(s => s.Course) // Asegúrate de incluir la información del curso
                .SingleOrDefaultAsync(s => s.ScheduleId == id);
        }

        public async Task<List<Schedule>> GetAllAsync()
        {
            return await _context.Schedules.Include(s => s.Course).ToListAsync();
        }

        public void Update(Schedule schedule) => _context.Schedules.Update(schedule);

        public void Delete(Schedule schedule) => _context.Schedules.Remove(schedule);

        public async Task<IReadOnlyList<Schedule>> GetByCourseIdAsync(Guid courseId)
        {
            return await _context.Schedules
                .Where(s => s.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Schedule>> GetByProfessorIdAsync(string professorId)
        {
            return await _context.Schedules
                .Where(s => s.ScheduleDetails.ProfessorId == professorId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Schedule>> GetBySchoolIdAsync(string schoolId)
        {
            return await _context.Schedules
                .Where(s => s.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Schedule>> GetByYearAsync(int year)
        {
            return await _context.Schedules
                .Where(s => s.Year == year)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetBySemesterYearAndSchoolAsync(int year, string semester, string schoolId)
        {
            return await _context.Schedules
                .Include(s => s.Course)
                .Where(s => s.Year == year && s.Course.Semester.Value == semester && s.SchoolId == schoolId)
                .ToListAsync();
        }

    }
}
