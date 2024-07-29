using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public class SchoolService : ISchoolService
{
    //de Repositories
    private readonly ISchoolRepository _schoolRepository;

    //Constructor
    public SchoolService(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }


    //Métodos

    public IEnumerable<School> GetAllSchools()
    {
        return _schoolRepository.GetAllSchools();
    }

    public School GetSchoolById(int id)
    {
        return _schoolRepository.GetSchoolById(id);
    }

    public string GetSchoolNameById(int id)
    {
        return _schoolRepository.GetSchoolNameById(id);
    }

    public void AddSchool(School school)
    {
        _schoolRepository.AddSchool(school);
    }

    public void UpdateSchool(School school)
    {
        _schoolRepository.UpdateSchool(school);
    }

    public void DeleteSchool(int id)
    {
        _schoolRepository.DeleteSchool(id);
    }
}
