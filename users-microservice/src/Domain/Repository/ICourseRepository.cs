using users_microservice.Domain.Entities;
using users_microservice.Domain.ValueObjects;
using static users_microservice.Application.Dtos.ServiceResponses;

namespace users_microservice.Domain.Repository
{
    public interface ICourseRepository
    {
        Task<GeneralResponse> AddCourseToStudent(CourseModel studentCourse);
        Task<IEnumerable<CourseInfo>> GetCoursesByStudentId(int studentId);
        Task<GeneralResponse> RemoveCourseFromStudent(int studentId, string courseId);
    }
}
