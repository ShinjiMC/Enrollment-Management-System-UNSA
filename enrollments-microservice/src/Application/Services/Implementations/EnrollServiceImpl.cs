using enrollments_microservice.Application.Dtos;
using enrollments_microservice.Domain.Entities;
using enrollments_microservice.Domain.ValueObjects;
using enrollments_microservice.Domain.Repositories;
using enrollments_microservice.Domain.Services.Interfaces;
using enrollments_microservice.Application.Services.Interfaces;
using static enrollments_microservice.Application.Dtos.ServiceResponses;
using enrollments_microservice.Application.Mapping;
using enrollments_microservice.Repositories.ExternalServices;

namespace enrollments_microservice.Application.Services.Implementations;
public class EnrollService : IEnrollService
{
    private readonly IEnrollRepository _enrollRepository;
    private readonly IEnrollServiceDomain _enrollServiceDomain;
    private readonly IUserExternalService _userExternalService;
    private readonly ISchoolExternalService _schoolExternalService;
    private readonly ICoursesExternalService _coursesExternalService;

    public EnrollService(IEnrollRepository enrollRepository, IEnrollServiceDomain enrollServiceDomain, IUserExternalService userExternalService, ISchoolExternalService schoolExternalService, ICoursesExternalService coursesExternalService)
    {
        _enrollRepository = enrollRepository;
        _enrollServiceDomain = enrollServiceDomain;
        _userExternalService = userExternalService;
        _schoolExternalService = schoolExternalService;
        _coursesExternalService = coursesExternalService;
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
        var user = await _userExternalService.GetDataByUserIdAsync(userId);
        if (user == null)
            return new GeneralResponse(false, "User not found", 404);
        if (user.Credits < enrollmentDto.Credits)
            return new GeneralResponse(false, "Insufficient credits", 400);
        enrollmentDto.FullName = user.FullName;
        enrollmentDto.AcademicPerformance = user.AcademicPerformance;
        var schoolName = await _schoolExternalService.GetNameBySchoolIdAsync(schoolId);
        if (schoolName == null)
            return new GeneralResponse(false, "School not found", 404);
        enrollmentDto.SchoolName = schoolName;
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

    public async Task<IEnumerable<EnrollmentDto>?> GetEnrollmentsByUserId(string userId)
    {
        var enroll = await _enrollServiceDomain.GetEnrollsByUserId(int.Parse(userId));
        if (enroll == null)
            return null;
        var enrollmentsDto = enroll.Select(EnrollMapping.ToDto);
        return enrollmentsDto;
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

    public async Task<IEnumerable<CourseExternalDto>?> GetAvailableCourses(string userId, string schoolId)
    {
        var user = await _userExternalService.GetDataByUserIdAsync(userId);
        if (user == null)
            return null;
        var school = await _schoolExternalService.GetCurriculumByIdAsync(schoolId);
        if (school == null || school.Curriculum == null)
            return null;
        var completedCourses = user.Courses ?? new List<string>();
        var completedCoursesSet = new HashSet<string>(completedCourses);
        var availableCourseIds = school.Curriculum
            .Where(c => (c.PreRequeriments ?? new List<string>()).All(preReq => completedCoursesSet.Contains(preReq)))
            .Select(c => c.Id)
            .ToList();
        if (availableCourseIds.Count == 0)
            return null;
        var availableCourseIdsFiltered = availableCourseIds
            .Where(courseId => !completedCoursesSet.Contains(courseId))
            .ToList();
        if (availableCourseIdsFiltered.Count == 0)
            return null;
        var courseDtos = new List<CourseExternalDto>();
        foreach (var courseId in availableCourseIdsFiltered)
        {
            if (courseId != null)
            {
                var course = await _coursesExternalService.GetScheduleOfCourseByIdAsync(courseId);
                if (course != null)
                    courseDtos.Add(course);
            }
        }
        return courseDtos;
    }
}