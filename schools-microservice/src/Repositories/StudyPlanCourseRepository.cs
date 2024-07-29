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

    public StudyPlanCourse GetStudyPlanById(int courseId)
    {
        var studyPlanCourse = _context.StudyPlanCourse.AsQueryable()
            .Where(c => c.Id == courseId)
            .Select(c => new StudyPlanCourse
            {
                Id = c.Id,
                Name = c.Name
            }).FirstOrDefault();

        if (studyPlanCourse != null)
        {
            studyPlanCourse.Prerequisites = _context.CoursePrerequisites.AsQueryable()
                .Where(p => p.CourseId == studyPlanCourse.Id)
                .Select(p => new CoursePrerequisite
                {
                    Id = p.Id,
                    CourseId = p.CourseId,
                    PrerequisiteCourseId = p.PrerequisiteCourseId
                }).ToList();
        }

        return studyPlanCourse;
    }

    public List<StudyPlanCourse> GetAllStudyPlans()
    {
        var studyPlanCourses = _context.StudyPlanCourse.AsQueryable()
            .Select(c => new StudyPlanCourse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

        foreach (var course in studyPlanCourses)
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

        return studyPlanCourses;
    }
    public void AddStudyPlanCourse(StudyPlanCourse studyPlanCourse)
    {
        _context.StudyPlanCourse.InsertOne(studyPlanCourse);
    }
}
