using Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Course course) =>_context.Courses.Add(course);
        public void Update(Course course) =>_context.Courses.Update(course);
        public void Delete(Course course) => _context.Courses.Remove(course);

        public async Task<bool> ExistsAsync(Guid id) => await _context.Courses.AnyAsync(course => course.CourseId == id);
        public async Task<Course?> GetByIdAsync(Guid courseId) => await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == courseId);
        public async Task<List<Course>> GetAll() => await _context.Courses.ToListAsync();
    }
}