using users_microservice.Domain.Entities;
using users_microservice.Domain.Factory;
using users_microservice.Domain.Repository;
using users_microservice.Domain.Services.Interfaces;
using static users_microservice.Application.Dtos.ServiceResponses;

namespace users_microservice.Domain.Services.Implementations
{
    public class StudentServiceDomainImpl : IStudentServiceDomain
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentServiceDomainImpl(ICourseRepository courseRepository, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _courseRepository = courseRepository;
        }

        public async Task<GeneralResponse> CreateStudent(StudentModel studentModel)
        {
            if (studentModel == null)
            {
                return new GeneralResponse(false, "Model is empty", 400,"-");
            }

            var createUserResult = await _adminRepository.CreateUser(studentModel);
            if (!createUserResult.Flag)
            {
                return createUserResult;
            }


            return createUserResult;
        }
        public async Task<GeneralResponse> CreateStudentCourse(List<CourseModel> courseModel)
        {
            if (courseModel == null)
            {
                return new GeneralResponse(false, "Model is empty", 400,"-");
            }

            // Crear una entrada en la tabla CourseModel
            List<string> ids;
            foreach (var course in courseModel)
            {
                var result =await _courseRepository.AddCourseToStudent(course);
                // ids.push
            }

            return new GeneralResponse(true, "Course created", 201,"-");
        }

        public async Task<GeneralResponse> UpdateStudent(StudentModel studentModel)
        {
           
            var student = await _adminRepository.GetStudentById(studentModel.Id);
            if (student == null)
            {
                return new GeneralResponse(false, "Student not found", 404,"-");
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
                return new GeneralResponse(false, "Student not found", 404,"-");
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

        
    }
}
