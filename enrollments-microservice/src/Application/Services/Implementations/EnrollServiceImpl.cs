using enrollments_microservice.Application.Dtos;
using enrollments_microservice.Domain.Entities;
using enrollments_microservice.Domain.ValueObjects;
using enrollments_microservice.Domain.Repositories;
using enrollments_microservice.Domain.Services.Interfaces;
using enrollments_microservice.Application.Services.Interfaces;
using static enrollments_microservice.Application.Dtos.ServiceResponses;
using enrollments_microservice.Application.Mapping;

namespace enrollments_microservice.Application.Services.Implementations;
public class EnrollService : IEnrollService
{
    private readonly IEnrollRepository _enrollRepository;
    private readonly IEnrollServiceDomain _enrollServiceDomain;

    public EnrollService(IEnrollRepository enrollRepository, IEnrollServiceDomain enrollServiceDomain)
    {
        _enrollRepository = enrollRepository;
        _enrollServiceDomain = enrollServiceDomain;
    }

    public async Task<IEnumerable<EnrollmentDto>?> GetEnrollments()
    {
        var enrolls = await _enrollRepository.GetEnrolls();
        if (enrolls == null)
            return null;
        var enrollmentDtos = enrolls.Select(EnrollMapping.ToDto);
        return enrollmentDtos;
    }

    public async Task<GeneralResponse> CreateEnrollment(string userId, string schoolId, EnrollmentDto enrollmentDto)
    {
        enrollmentDto.SchoolId = schoolId;
        enrollmentDto.StudentId = userId;
        var enrollModel = EnrollMapping.ToModel(enrollmentDto);
        var result = await _enrollServiceDomain.CreateEnroll(enrollModel);
        if (!result.Flag)
            return result;
        return new GeneralResponse(true, "Enrollment created successfully", 201);
    }

    public async Task<GeneralResponse> UpdateEnrollment(string id, EnrollmentDto enrollmentDto)
    {
        var enrollModel = EnrollMapping.ToModel(enrollmentDto);
        int enrollId = int.Parse(id);
        enrollModel.Id = enrollId;
        var result = await _enrollServiceDomain.UpdateEnroll(enrollModel);
        return result;
    }

    public async Task<GeneralResponse> DeleteEnrollment(string enrollId)
    {
        var result = await _enrollServiceDomain.DeleteEnroll(int.Parse(enrollId));
        return result;
    }

    public async Task<EnrollmentDto> GetEnrollmentById(string enrollId)
    {
        var enroll = await _enrollServiceDomain.GetEnrollById(int.Parse(enrollId));
        if (enroll == null)
            return new EnrollmentDto();
        var enrollmentDto = EnrollMapping.ToDto(enroll);
        return enrollmentDto;
    }

    public async Task<EnrollmentDto> GetEnrollmentByUserId(string userId)
    {
        var enroll = await _enrollServiceDomain.GetEnrollByUserId(int.Parse(userId));
        if (enroll == null)
            return new EnrollmentDto();
        var enrollmentDto = EnrollMapping.ToDto(enroll);
        return enrollmentDto;
    }

    public async Task<EnrollmentDto> GetEnrollmentByUserIdAndSchoolId(string userId, string schoolId)
    {
        var enroll = await _enrollServiceDomain.GetEnrollByUserIdAndSchoolId(int.Parse(userId), int.Parse(schoolId));
        if (enroll == null)
            return new EnrollmentDto();
        var enrollmentDto = EnrollMapping.ToDto(enroll);
        return enrollmentDto;
    }

    public async Task<IEnumerable<EnrollmentDto>?> GetEnrollmentsBySchoolId(string schoolId)
    {
        var enrolls = await _enrollServiceDomain.GetEnrollsBySchoolId(int.Parse(schoolId));
        if (enrolls == null)
            return null;
        var enrollmentDtos = enrolls.Select(EnrollMapping.ToDto);
        return enrollmentDtos;
    }

    public async Task<IEnumerable<CourseDto>?> GetAvailableCourses(string userId, string schoolId)
    {
        var enroll = await _enrollServiceDomain.GetEnrollByUserIdAndSchoolId(int.Parse(userId), int.Parse(schoolId));
        if (enroll == null)
            return null;
        if (enroll.Courses == null)
            return null;
        var courseDtos = enroll.Courses.Select(CourseMapping.ToDto);
        return courseDtos;
    }
}