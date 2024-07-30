using users_microservice.Application.Dtos;
using users_microservice.Application.Mapping;
using users_microservice.Application.Service.Interface;
using users_microservice.Domain.Repository;
using users_microservice.Domain.Services.Interfaces;
using users_microservice.Repository.ExternalService;
using static users_microservice.Application.Dtos.ServiceResponses;


namespace users_microservice.Application.Service.Implementations
{

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminServiceDomain _adminServiceDomain;
         private readonly IExternalServiceAuth _externalService;

        public AdminService(IAdminRepository adminRepository, IAdminServiceDomain adminServiceDomain, IExternalServiceAuth externalService)
        {
            _adminRepository = adminRepository;
            _adminServiceDomain = adminServiceDomain;
            _externalService = externalService;
        }

        public async Task<GeneralResponse> CreateAdminAccountAsync(AdminDto adminDto)
        {
            // Crear el modelo de administrador a partir del DTO
            var adminModel = AdminMapping.ToModel(adminDto);
            
            // Iniciar la transacción
            await _adminRepository.BeginTransactionAsync();

            try
            {
                // Crear la cuenta de administrador en la base de datos
                await _adminServiceDomain.CreateAdminAccount(adminModel);

                // Crear el DTO del usuario para el servicio externo
                var newUserDto = UserMapping.ToUserDto(adminDto);

                // Registrar el usuario en el servicio externo
                // var authCreationResult = await _externalService.RegisterUserAsync(newUserDto);

                // if (!authCreationResult)
                // {
                //     // Si la creación de usuario en el servicio externo falla, revertir la transacción
                //     await _adminRepository.RollbackTransactionAsync();
                //     return new GeneralResponse(false, "Fail to connect external service", 500);
                // }

                // Confirmar la transacción si  ha salido bien
                await _adminRepository.CommitTransactionAsync();
                
                return new GeneralResponse(true, "Admin user created successfully", 201, adminModel.Id.ToString() );
            }
            catch (Exception ex)
            {
                // En caso de excepción, revertir la transacción
                await _adminRepository.RollbackTransactionAsync();
                // Opcional: loguear la excepción
                
                return new GeneralResponse(false, ex.ToString() , 500, "-");
            }
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
            if(string.IsNullOrEmpty(adminDto.Id))
                return new GeneralResponse(false, "User not found", 404, "-");
            
            var idUser = int.Parse(adminDto.Id);

            await _adminRepository.BeginTransactionAsync();

            try
            {
                
                var user = await _adminServiceDomain.GetAdminById(idUser);

                
                if (user == null || string.IsNullOrEmpty(user.FullName))
                {
                    await _adminRepository.RollbackTransactionAsync();
                    return new GeneralResponse(false, "User not found", 404, "-");
                }
               

                user.FullName = adminDto.FullName;
                user.Email = adminDto.Email;
                user.PhoneNumber = adminDto.PhoneNumber;

                var result = await _adminRepository.UpdateUser(user);

                if (result == null)
                {
                    await _adminRepository.RollbackTransactionAsync();
                    return new GeneralResponse(false, "Error", 204, "-");
                }

                await _adminRepository.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _adminRepository.RollbackTransactionAsync();
                return new GeneralResponse(false, $"Error: {ex.Message}", 500, "-");
            }
        }

        public async Task<GeneralResponse> DeleteAdminAccountAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return new GeneralResponse(false, "Error Delete", 204,"-");
            }

            int adminId = int.Parse(id);
            var result = await _adminServiceDomain.DeleteAdminAccount(adminId);

            return result;
        }

       public async Task<GeneralResponse> CreateStudentAsync(StudentDto studentDto)
        {
            // Mapear el DTO a un modelo de estudiante
            var studentModel = StudentMapping.ToModel(studentDto);
            
            // Iniciar la transacción
            await _adminRepository.BeginTransactionAsync();

            try
            {
                // Crear el estudiante en el dominio
                var result = await _adminServiceDomain.CreateStudent(studentModel);

                if (!result.Flag)
                {
                    // Si la creación del estudiante falla, devolver el resultado inmediatamente
                    await _adminRepository.RollbackTransactionAsync();
                    return result;
                }

                // Mapear los cursos desde el DTO usando el CourseMapping
                var courses = studentDto.CourseIds.Select(courseId => CourseMapping.ToModel(courseId, studentModel.Id)).ToList();

                // Crear los cursos en el dominio
                var courseCreationResult = await _adminServiceDomain.CreateStudentCourse(courses);

                if (!courseCreationResult.Flag)
                {
                    // Si la creación de cursos falla, devolver el resultado de error
                    await _adminRepository.RollbackTransactionAsync();
                    return courseCreationResult;
                }

                var newUserDto = UserMapping.ToUserDto(studentDto);

                // var authCreationResult = await _externalService.RegisterUserAsync(newUserDto);

                // if (!authCreationResult)
                // {
                //     // Si la creación en el servicio externo falla, devolver el resultado de error
                //     await _adminRepository.RollbackTransactionAsync();
                //     return new GeneralResponse(false, "Auth failed to created", 404, "-");
                // }

                // Confirmar la transacción si todo ha salido bien
                await _adminRepository.CommitTransactionAsync();
                
                // Devolver el resultado exitoso incluyendo los datos del estudiante y los cursos
                return result;
            }
            catch (Exception ex)
            {
                // En caso de excepción, revertir la transacción
                await _adminRepository.RollbackTransactionAsync();
                
                // Opcional: loguear la excepción
                return new GeneralResponse(false, $"Error: {ex.Message}", 500, "-");
            }
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
    try
    {
        if (string.IsNullOrEmpty(id))
        {
            return new GeneralResponse(false, "ERROR: ID is null or empty", 204, "-");
        }

        if (!int.TryParse(id, out int studentId))
        {
            return new GeneralResponse(false, "ERROR: Invalid ID format", 400, "-");
        }

        var result = await _adminServiceDomain.DeleteStudent(studentId);
        return result;
    }
    catch (Exception ex)
    {
        // Log the exception (assuming you have a logger)
        

        return new GeneralResponse(false, ex.ToString() , 500, "-");
    }
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