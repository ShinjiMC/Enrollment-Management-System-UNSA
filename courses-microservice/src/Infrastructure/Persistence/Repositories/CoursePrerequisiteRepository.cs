using Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CoursePrerequisiteRepository : ICoursePrerequisiteRepository
{
    private readonly ApplicationDbContext _context;

    public CoursePrerequisiteRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<List<CoursePrerequisite>> GetPrerequisitesByCourseIdAsync(Guid courseId)
    {
        return await _context.CoursePrerequisites
            .Where(cp => cp.CourseId == courseId)
            .ToListAsync();
    }
    public void Add(CoursePrerequisite course) =>_context.CoursePrerequisites.Add(course);
    public void Update(CoursePrerequisite course) =>_context.CoursePrerequisites.Update(course);
    public void Delete(CoursePrerequisite course) =>_context.CoursePrerequisites.Remove(course);
    public async Task<CoursePrerequisite?> GetByIdAsync(Guid courseId) => await _context.CoursePrerequisites.SingleOrDefaultAsync(c => c.CourseId == courseId);
}