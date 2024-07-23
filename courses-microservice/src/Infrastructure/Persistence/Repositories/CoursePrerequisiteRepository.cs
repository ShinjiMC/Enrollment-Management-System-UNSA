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
    public async Task<List<CoursePrerequisite>> GetPrerequisitesByCourseIdAsync(CourseId courseId)
    {
        return await _context.CoursePrerequisites
            .Where(cp => cp.CourseId == courseId)
            .ToListAsync();
    }
    public void Add(CoursePrerequisite prerequisite) =>_context.CoursePrerequisites.Add(prerequisite);
    public void Update(CoursePrerequisite prerequisite) =>_context.CoursePrerequisites.Update(prerequisite);
    public void Delete(CoursePrerequisite prerequisite) =>_context.CoursePrerequisites.Remove(prerequisite);
    public async Task<CoursePrerequisite?> GetByIdAsync(CourseId courseId) => await _context.CoursePrerequisites.SingleOrDefaultAsync(c => c.CourseId == courseId);
}