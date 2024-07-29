using MongoDB.Driver;
using SchoolsMicroservice.Models;
using SchoolsMicroservice.Repositories.Data;
namespace SchoolsMicroservice.Service;

public class SchoolRepository : ISchoolRepository
{
    private readonly MongoDbContext _context;

    public SchoolRepository(MongoDbContext context)
    {
        _context = context;
    }

    public IEnumerable<School> GetAllSchools()
    {
        return _context.Schools.Find(school => true).ToList();
    }

    public School GetSchoolById(int id)
    {
        return _context.Schools.Find(school => school.Id == id).FirstOrDefault();
    }

    public void AddSchool(School school)
    {
        _context.Schools.InsertOne(school);
    }

    public void UpdateSchool(School school)
    {
        _context.Schools.ReplaceOne(s => s.Id == school.Id, school);
    }

    public void DeleteSchool(int id)
    {
        _context.Schools.DeleteOne(school => school.Id == id);
    }
}
