using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;
public interface ISchoolRepository
{
    IEnumerable<School> GetAllSchools();
    School GetSchoolById(int id);
    string GetSchoolNameById(int id);
    void AddSchool(School school);
    void UpdateSchool(School school);
    void DeleteSchool(int id);
}
