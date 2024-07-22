using users_microservice.Application.Dtos;
using users_microservice.Application.Mapping;
using users_microservice.Application.Service.Interface;
using users_microservice.Domain.Repository;
using users_microservice.Domain.Services.Interfaces;
using static users_microservice.Application.Dtos.ServiceResponses;


namespace users_microservice.Application.Service.Implementations
{

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminServiceDomain _adminServiceDomain;

        public AdminService(IAdminRepository adminRepository, IAdminServiceDomain adminServiceDomain)
        {
            _adminRepository = adminRepository;
            _adminServiceDomain = adminServiceDomain;
        }

        public async Task<GeneralResponse> CreateAdminAccountAsync(AdminDto adminDto)
        {
            var adminModel = AdminMapping.ToModel(adminDto);
            
            var result = await _adminServiceDomain.CreateAdminAccount(adminModel);

            return result;
        }

        public async Task<AdminDto> RetrieveAdminAccountAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new AdminDto();
            }

            int adminId = int.Parse(id);

            var result = await _adminServiceDomain.GetAdminById(adminId);

            var adminNew = AdminMapping.ToDto(result);

            return adminNew;
        }

        public async Task<GeneralResponse> UpdateAdminAccountAsync(AdminDto adminDto)
        {
            var adminModel = AdminMapping.ToModel(adminDto);

            var result = await _adminServiceDomain.UpdateAdminAccount(adminModel);

            return result;
        }

        public async Task<GeneralResponse> DeleteAdminAccountAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return new GeneralResponse(false, "Error Delete", 204);
            }

            int adminId = int.Parse(id);
            var result = await _adminServiceDomain.DeleteAdminAccount(adminId);

            return result;
        }

       public async Task<GeneralResponse> CreateStudentAsync(StudentDto studentDto)
        {
            // Mapear el DTO a un modelo de estudiante
            var studentModel = StudentMapping.ToModel(studentDto);
            
            // Crear el estudiante en el dominio
            var result = await _adminServiceDomain.CreateStudent(studentModel);

            if (!result.Flag) {
                // Si la creación del estudiante falla, devuelve el resultado inmediatamente
                return result;
            }
            // Obtener el id generado

            
            // Mapear los cursos desde el DTO usando el CourseMapping
            var courses = studentDto.CourseIds.Select(courseId => CourseMapping.ToModel(courseId, studentModel.Id)).ToList();

            // Crear los cursos en el dominio
            var courseCreationResult = await _adminServiceDomain.CreateStudentCourse(courses);

            if (!courseCreationResult.Flag) {
                // Si la creación de cursos falla, devuelve el resultado de error
                return courseCreationResult;
            }

            // Devolver el resultado exitoso incluyendo los datos del estudiante y los cursos
            return new GeneralResponse
            (
                true,
                 "Student and courses created successfully.",
                204
            );
        }

        public async Task<GeneralResponse> UpdateStudentAsync(string id, StudentDto studentDto)
        {
            var studentModel = StudentMapping.ToModel(studentDto);
            
            int studentId = int.Parse(id);
            studentModel.Id = studentId; // Asegurar que el ID es el correcto
            var result = await _adminServiceDomain.UpdateStudent(studentModel);
            return result;
        }

        public async Task<GeneralResponse> DeleteStudentAsync(string id)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                return new GeneralResponse(false,"ERROR", 204);
            }

            int studentId = int.Parse(id);

            var result = await _adminServiceDomain.DeleteStudent(studentId);
            return result;
        }

        public async Task<StudentDto?> GetStudentAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new StudentDto();
            }

            int studentId = int.Parse(id);

            var student = await _adminServiceDomain.GetStudent(studentId);
            return student != null ? StudentMapping.ToDto(student) : null;
        }

        public async Task<StudentDto> GetCoursesByStudentIdAsync(string id)
        {
             if (string.IsNullOrEmpty(id))
            {
                return new StudentDto();
            }

            int adminId = int.Parse(id);

            var courses = await _adminServiceDomain.GetCoursesByStudentId(adminId);

            var studnetCourses = StudentMapping.ToDto(courses);
            

            return studnetCourses;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
           var students = await _adminRepository.GetAllStudents();
            var studentDtos = students.Select(student => StudentMapping.ToDto(student)); // Llamar explícitamente a StudentMapping.ToDto
            return studentDtos;
        }

        public async Task<IEnumerable<AdminDto>> GetAllAdminsAsync()
        {
            var admins = await _adminRepository.GetAllAdmins();
            var adminDtos = admins.Select(admin => AdminMapping.ToDto(admin)); // Llamar explícitamente a AdminMapping.ToDto
            return adminDtos;
        }
    }
}