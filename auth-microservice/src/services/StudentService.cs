using users_microservice.models;
using users_microservice.repositories;

namespace users_microservice.services;

public interface IStudentService 
{
    IEnumerable<StudentModel> GetAllStudents();
}

public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<StudentModel> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }
    }