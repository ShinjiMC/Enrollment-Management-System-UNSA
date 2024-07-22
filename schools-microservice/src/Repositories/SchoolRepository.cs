
/*
 implementando la interfaz ISchoolRepository
*/
public class SchoolRepository : ISchoolRepository
{
    
    //para almacenar todas las escuelas del sistema
    private readonly List<School> _schools = new();

    public IEnumerable<School> GetAllSchools()
    {
        return _schools;
    }

    public School GetSchoolById(int id)
    {
        return _schools.FirstOrDefault(s => s.Id == id);
    }

    public void AddSchool(School school)
    {
        _schools.Add(school);
    }

    public void UpdateSchool(School school)
    {
        var existingSchool = GetSchoolById(school.Id);
        if (existingSchool != null)
        {
            existingSchool.Name = school.Name;
            existingSchool.Location = school.Location;
        }
    }

    public void DeleteSchool(int id)
    {
        var school = GetSchoolById(id);
        if (school != null)
        {
            _schools.Remove(school);
        }
    }
}
