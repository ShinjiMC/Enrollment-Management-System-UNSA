using users_microservice.Domain.Entities;
using users_microservice.Domain.Factory;
using users_microservice.Domain.Repository;
using users_microservice.Domain.Services.Interfaces;
using static users_microservice.Application.Dtos.ServiceResponses;

namespace users_microservice.Domain.Services.Implementations;

public class AdminServiceDomainImpl: IAdminServiceDomain
{
    private readonly IAdminRepository _adminRepository;
    private readonly ICourseRepository _courseRepository;

    public AdminServiceDomainImpl(IAdminRepository adminRepository, ICourseRepository courseRepository)
    {
        _adminRepository = adminRepository;
        _courseRepository = courseRepository;
    }

    public async Task<GeneralResponse> CreateAdminAccount(AdminModel userDto)
    {
        if (userDto == null)
        {
            return new GeneralResponse(false, "Model is empty", 400);
        }

        var createUserResult = await _adminRepository.CreateUser(userDto);
        if (!createUserResult.Flag)
        {
            return new GeneralResponse(false, createUserResult.Message, 403);
        }

        return new GeneralResponse(true, "Account created with Admin role", 200);
    }

    public async Task<GeneralResponse> UpdateAdminAccount(AdminModel userDto)
    {
        
        var user = await _adminRepository.GetUserById(userDto.Id);
        if (user == null)
        {
            return new GeneralResponse(false, "User not found", 404);
        }

        user.FullName = userDto.FullName;
        user.Email = userDto.Email;

        var result = await _adminRepository.UpdateUser(user);

        // Si UpdateUser podría devolver null, maneja la nulabilidad
        if (result == null)
        {
            return new GeneralResponse(false, "Error", 204);
        }

        // Asegúrate de que el tipo devuelto sea compatible con el tipo esperado
        return result;
    }

    public async Task<GeneralResponse> DeleteAdminAccount(int id)
    {
        
        var user = await _adminRepository.GetUserById(id);
        if (user == null)
        {
            return new GeneralResponse(false, "User not found", 404);
        }

        var result = await _adminRepository.DeleteUser(user);

        return result;
    }

    // Métodos para manejar estudiantes
    public async Task<GeneralResponse> CreateStudent(StudentModel studentModel)
    {
        if (studentModel == null)
        {
            return new GeneralResponse(false, "Model is empty", 400);
        }

        var createStudentResult = await _adminRepository.CreateStudent(studentModel);
        if (!createStudentResult.Flag)
        {
            return new GeneralResponse(false, createStudentResult.Message, 403);
        }

        return new GeneralResponse(true, "Student created successfully", 200);
    }
    public async Task<GeneralResponse> CreateStudentCourse(List<CourseModel> courseModel)
        {
            if (courseModel == null)
            {
                return new GeneralResponse(false, "Model is empty", 400);
            }

            // Crear una entrada en la tabla CourseModel
            foreach (var course in courseModel)
            {
                await _courseRepository.AddCourseToStudent(course);
            }

            return new GeneralResponse(true, "Course created", 201);
        }

    public async Task<GeneralResponse> UpdateStudent(StudentModel studentModel)
    {
        // Verificar que el Id es un entero y convertirlo a cadena si es necesario
        string studentId = studentModel.Id.ToString();

        if (string.IsNullOrEmpty(studentId)) // Asegúrate de que el Id no esté vacío o nulo
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        var student = await _adminRepository.GetStudentById(studentModel.Id);
        if (student == null)
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        student.FullName = studentModel.FullName;
        student.Email = studentModel.Email;

        var result = await _adminRepository.UpdateStudent(student);

        return result;
    }


    public async Task<GeneralResponse> DeleteStudent(int id)
    {
        var student = await _adminRepository.GetStudentById(id);
        if (student == null)
        {
            return new GeneralResponse(false, "Student not found", 404);
        }

        var result = await _adminRepository.DeleteStudent(student);

        return result;
    }

    public async Task<StudentModel?> GetStudent(int id)
    {
        var student = await _adminRepository.GetStudentById(id);
        return student;
    }

    public async Task<StudentModel> GetCoursesByStudentId(int id)
    {
        var courses = (await _courseRepository.GetCoursesByStudentId(id)).ToList();

        var newStudent = await _adminRepository.GetStudentById(id);
        if (newStudent == null)
        {
            return new StudentModel();
        }

        var ad = new StudentCoursesFactory(); // O la forma en que lo inicialices apropiadamente

        var studentCourses = ad.CreateStudentCourses(newStudent, courses);
        return studentCourses;
    }


    public Task<AdminModel> GetAdminById(int id)
    {
        throw new NotImplementedException();
    }
}