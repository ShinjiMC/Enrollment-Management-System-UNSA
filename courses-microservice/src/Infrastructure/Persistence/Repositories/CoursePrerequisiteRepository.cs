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
    public void Add(CoursePrerequisite course) => _context.CoursePrerequisites.Add(course);
    public void Update(CoursePrerequisite course) => _context.CoursePrerequisites.Update(course);
    public async Task<bool> DeleteAsync(Guid courseId, Guid prerequisiteCourseId)
    {
        var coursePrerequisite = await _context.CoursePrerequisites
            .FindAsync(courseId, prerequisiteCourseId);

        if (coursePrerequisite == null)
        {
            return false; 
        }

        _context.CoursePrerequisites.Remove(coursePrerequisite);
        await _context.SaveChangesAsync();

        return true; 
    }
    public async Task<CoursePrerequisite?> GetByIdAsync(Guid courseId, Guid coursePrerequisiteId) => await _context.CoursePrerequisites.FindAsync(courseId, coursePrerequisiteId);
}