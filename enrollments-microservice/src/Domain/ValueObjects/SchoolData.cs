namespace enrollments_microservice.Domain.ValueObjects;

public class SchoolData
{
    public int SchoolID { get; private set; }
    public string Name { get; private set; }

    public SchoolData(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("School ID must be greater than 0 of SchoolData");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name must not be empty of SchoolData");
        SchoolID = id;
        Name = name;
    }

    // Opcional: Método para comparar dos instancias de SchoolData
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        var schoolData = (SchoolData)obj;
        return SchoolID == schoolData.SchoolID &&
               Name == schoolData.Name;
    }

    // Opcional: Método para obtener el código hash de una instancia de SchoolData
    public override int GetHashCode()
    {
        return HashCode.Combine(SchoolID, Name);
    }
}