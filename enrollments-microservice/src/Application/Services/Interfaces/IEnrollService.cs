using enrollments_microservice.Application.Dtos;
using static enrollments_microservice.Application.Dtos.ServiceResponses;

namespace enrollments_microservice.Application.Services.Interfaces;
public interface IEnrollService
{
    Task<IEnumerable<EnrollmentDto>?> GetEnrollments();
    Task<GeneralResponse<IEnumerable<CourseExternalDto>?>> GetAvailableCourses(string userId, string schoolId);
    Task<GeneralResponse<IEnumerable<EnrollmentDto>?>> GetEnrollmentsByUserId(string userId);
    Task<GeneralResponse<IEnumerable<EnrollmentDto>?>> GetEnrollmentsBySchoolId(string schoolId);
    Task<GeneralResponse<IEnumerable<EnrollmentDto>?>> GetEnrollmentByUserIdAndSchoolId(string userId, string schoolId);
    Task<EnrollmentDto?> GetEnrollmentById(string enrollId);
    Task<GeneralResponse<EnrollmentDto>> CreateEnrollment(string userId, string schoolId, EnrollmentDto enrollmentDto);
    Task<GeneralResponse> UpdateEnrollment(string id, EnrollmentDto enrollmentDto);
    Task<GeneralResponse> DeleteEnrollment(string enrollId);
}