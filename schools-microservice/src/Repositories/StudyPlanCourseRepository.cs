using MongoDB.Driver;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Repositories.Data;
namespace SchoolsMicroservice.Service;

public class StudyPlanCourseRepository : IStudyPlanCourseRepository
{
    private readonly MongoDbContext _context;

    public StudyPlanCourseRepository(MongoDbContext context)
    {
        _context = context;
    }

    public StudyPlanCourse GetCourseWithPrerequisites(int courseId)
    {
        var course = _context.Courses.AsQueryable()
            .Where(c => c.Id == courseId)
            .Select(c => new StudyPlanCourse
            {
                Id = c.Id,
                Name = c.Name
            }).FirstOrDefault();

        if (course != null)
        {
            course.Prerequisites = _context.CoursePrerequisites.AsQueryable()
                .Where(p => p.CourseId == course.Id)
                .Select(p => new CoursePrerequisite
                {
                    Id = p.Id,
                    CourseId = p.CourseId,
                    PrerequisiteCourseId = p.PrerequisiteCourseId
                }).ToList();
        }

        return course;
    }

    public List<StudyPlanCourse> GetAllCoursesWithPrerequisites()
    {
        var courses = _context.Courses.AsQueryable()
            .Select(c => new StudyPlanCourse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

        foreach (var course in courses)
        {
            course.Prerequisites = _context.CoursePrerequisites.AsQueryable()
                .Where(p => p.CourseId == course.Id)
                .Select(p => new CoursePrerequisite
                {
                    Id = p.Id,
                    CourseId = p.CourseId,
                    PrerequisiteCourseId = p.PrerequisiteCourseId
                }).ToList();
        }

        return courses;
    }
    public void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse)
    {
        _context.StudyPlanCourse.InsertOne(studyPlanCourse);
    }
}
