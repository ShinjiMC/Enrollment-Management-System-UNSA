using users_microservice.Application.Dtos;
using users_microservice.Application.Mapping;
using users_microservice.Application.Service.Interface;
using users_microservice.Domain.Repository;
using users_microservice.Domain.Services.Interfaces;
using static users_microservice.Application.Dtos.ServiceResponses;

namespace users_microservice.Application.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IStudentServiceDomain _studentServiceDomain;
        private readonly IExternalService _externalService;
        
        public StudentService(IAdminRepository adminRepository, IStudentServiceDomain studentServiceDomain, IExternalService externalService)
        {
            _adminRepository = adminRepository;
            _studentServiceDomain = studentServiceDomain;
            _externalService = externalService;
        }

        public async Task<GeneralResponse> CreateStudentAccount(StudentDto studentDto)
        {
            // Mapear el DTO a un modelo de estudiante
            var studentModel = StudentMapping.ToModel(studentDto);
            
            // Crear el estudiante en el dominio
            var result = await _studentServiceDomain.CreateStudent(studentModel);

            if (!result.Flag) {
                // Si la creación del estudiante falla, devuelve el resultado inmediatamente
                return result;
            }

            // Mapear los cursos desde el DTO usando el CourseMapping
            var courses = studentDto.CourseIds.Select(courseId => CourseMapping.ToModel(courseId, studentModel.Id)).ToList();

            // Crear los cursos en el dominio
            var courseCreationResult = await _studentServiceDomain.CreateStudentCourse(courses);

            if (!courseCreationResult.Flag) {
                // Si la creación de cursos falla, devuelve el resultado de error
                return courseCreationResult;
            }

            var newUserDto = UserMapping.ToUserDto(studentDto);

            var authCreationResult = await _externalService.RegisterUserAsync(newUserDto);

            if (!authCreationResult) {
                // Si la creación de cursos falla, devuelve el resultado de error
                return new GeneralResponse(false, "Auth failed to created", 404);
            }

            // Devolver el resultado exitoso incluyendo los datos del estudiante y los cursos
            return new GeneralResponse
            (
                true,
                 "Student and courses created successfully.",
                204
            );
        }


        public async Task<GeneralResponse> DeleteStudentById(string id)
        {
            var stdId = int.Parse(id);
            var result = await _studentServiceDomain.DeleteStudent(stdId);
            return result;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            var students = await _adminRepository.GetAllStudents();
            var studentDtos = students.Select(student => StudentMapping.ToDto(student));
            return studentDtos;
        }

        public async Task<StudentDto> GetCoursesByStudentIdAsync(string studentId)
        {
            var stdId = int.Parse(studentId);

            var result = await _studentServiceDomain.GetCoursesByStudentId(stdId);

            if (result == null)
            {
                return new StudentDto();
            }

            var studentCourses = StudentMapping.ToDto(result);
            return studentCourses;
        }

        public async Task<StudentDto> GetStudentAsync(string id)
        {
            var stdId = int.Parse(id);
            var student = await _studentServiceDomain.GetStudent(stdId);
            if (student == null)
            {
                return new StudentDto();
            }

            var studentDto = StudentMapping.ToDto(student);
            return studentDto;
        }

        public async Task<GeneralResponse> UpdateStudentById(StudentDto studentDto)
        {
            var studentModel = StudentMapping.ToModel(studentDto);
            var result = await _studentServiceDomain.UpdateStudent(studentModel);
            return result;
        }

        
    }
}
