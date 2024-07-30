using MongoDB.Driver;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Repositories.Data;
namespace SchoolsMicroservice.Service;


public class StudyPlanSchoolRepository : IStudyPlanSchoolRepository
{
    private readonly MongoDbContext _context;

    public StudyPlanSchoolRepository(MongoDbContext context)
    {
        _context = context;
    }

    public StudyPlanSchool GetStudyPlanBySchoolName(string schoolName)
    {
        return _context.StudyPlanSchools.AsQueryable()
            .FirstOrDefault(s => s.Name == schoolName);
    }

    public StudyPlanSchool GetStudyPlanBySchoolId(int schoolId)
    {
        return _context.StudyPlanSchools.AsQueryable()
            .FirstOrDefault(s => s.Id == schoolId);
    }
    

    public void AddStudyPlanSchool(StudyPlanSchool studyPlanSchool)
    {
        _context.StudyPlanSchools.InsertOne(studyPlanSchool);
    }
}
