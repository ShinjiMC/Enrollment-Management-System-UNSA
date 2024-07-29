
using SchoolsMicroservice.Models;
namespace SchoolsMicroservice.Service;

public interface ISchoolService
{
    /*
    Recuperar una lista de todas las escuelas del sistema, 
    devolverá una colección de objetos de tipo School.
    */
    IEnumerable<School> GetAllSchools();
    
    /*
    Este método recupera una escuela específica por su identificador (id).
    */
    School GetSchoolById(int id);
    
    /*
    Agregar una nueva escuela al sistema.
    */
    void AddSchool(School school);

    /*
    Este método actualiza los datos de una escuela existente. Toma un objeto 
    School como argumento que contiene la información actualizada del objeto School.
    */
    void UpdateSchool(School school);

    /*
    Eliminar por id
    */
    void DeleteSchool(int id);
}
