using enrollments_microservice.Domain.Entities;
using enrollments_microservice.Domain.ValueObjects;
using static enrollments_microservice.Application.Dtos.ServiceResponses;

namespace enrollments_microservice.Domain.Services.Interfaces;
public interface IEnrollServiceDomain
{
    Task<GeneralResponse> CreateEnroll(EnrollModel enrollModel);
    Task<EnrollModel?> GetEnrollById(int id);
    Task<List<EnrollModel>?> GetEnrollsByUserId(int userId);
    Task<EnrollModel?> GetEnrollByUserIdAndSchoolId(int userId, int schoolId);
    Task<List<EnrollModel>?> GetEnrollsBySchoolId(int schoolId);
    Task<List<EnrollModel>?> GetEnrolls();
    Task<GeneralResponse> UpdateEnroll(EnrollModel enrollModel);
    Task<GeneralResponse> DeleteEnroll(int id);
}