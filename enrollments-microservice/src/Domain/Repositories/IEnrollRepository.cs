namespace enrollments_microservice.Domain.Repositories;
using enrollments_microservice.Domain.Entities;
using static enrollments_microservice.Application.Dtos.ServiceResponses;

public interface IEnrollRepository
{
    Task<GeneralResponse> CreateEnroll(EnrollModel enrollModel);
    Task<EnrollModel> GetEnrollById(int id);
    Task<List<EnrollModel>> GetEnrollsByUserId(int userId);
    Task<EnrollModel> GetEnrollByUserIdAndSchoolId(int userId, int schoolId);
    Task<List<EnrollModel>> GetEnrollsBySchoolId(int schoolId);
    Task<List<EnrollModel>> GetEnrolls();
    Task<GeneralResponse> UpdateEnroll(EnrollModel enrollModel);
    Task<GeneralResponse> DeleteEnroll(int id);
}