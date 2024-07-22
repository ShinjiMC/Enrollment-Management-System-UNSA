
//Solo métodos
public interface ISchoolRepository
{
    IEnumerable<School> GetAllSchools();
    School GetSchoolById(int id);
    void AddSchool(School school);
    void UpdateSchool(School school);
    void DeleteSchool(int id);
}
