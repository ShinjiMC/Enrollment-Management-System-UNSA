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

    public async Task<GeneralResponse<EnrollmentDto>> CreateEnrollment(string userId, string schoolId, EnrollmentDto enrollmentDto)
    {
        enrollmentDto.SchoolId = schoolId;
        enrollmentDto.StudentId = userId;
        var user = await _userExternalService.GetDataByUserIdAsync(userId);
        if (user == null)
            return new GeneralResponse<EnrollmentDto>(false, Messages.UserNotFound, 404, new EnrollmentDto());
        enrollmentDto.FullName = user.FullName;
        enrollmentDto.AcademicPerformance = user.AcademicPerformance;
        var schoolName = await _schoolExternalService.GetNameBySchoolIdAsync(schoolId);
        if (schoolName == null)
            return new GeneralResponse<EnrollmentDto>(false, Messages.SchoolNotFound, 404, new EnrollmentDto());
        enrollmentDto.SchoolName = schoolName;
        if (enrollmentDto.Courses == null || enrollmentDto.Courses.Count == 0)
            return new GeneralResponse<EnrollmentDto>(false, "No courses to enroll", 400, new EnrollmentDto());
        var totalCredits = 0;
        foreach (var course in enrollmentDto.Courses)
        {
            if (course == null || course.Id == null)
                return new GeneralResponse<EnrollmentDto>(false, "Invalid course ID", 400, new EnrollmentDto());
            var courseExternal = await _coursesExternalService.GetCreditsOfCourseByIdAsync(course.Id, schoolId);
            if (courseExternal == null || courseExternal.Credits == null)
                return new GeneralResponse<EnrollmentDto>(false, "Course not found or credits missing", 404, new EnrollmentDto());
            totalCredits += (int)courseExternal.Credits;
        }
        if (totalCredits > user.Credits)
            return new GeneralResponse<EnrollmentDto>(false, "Insufficient credits", 400, new EnrollmentDto());
        enrollmentDto.Credits = totalCredits;
        var enrollModel = EnrollMapping.ToModel(enrollmentDto);
        var result = await _enrollServiceDomain.CreateEnroll(enrollModel);
        if (result == null)
            return new GeneralResponse<EnrollmentDto>(false, "Failed to create enrollment", 500, new EnrollmentDto());
        var enrollmentSaveDto = EnrollMapping.ToDto(result);
        return new GeneralResponse<EnrollmentDto>(true, "Enrollment created successfully", 201, enrollmentSaveDto);
    }

    public async Task<GeneralResponse> UpdateEnrollment(string id, EnrollmentDto enrollmentDto)
    {
        if (string.IsNullOrEmpty(id) || enrollmentDto == null)
            return new GeneralResponse(false, "Enroll Id is required", 400);
        var enrollData = await _enrollServiceDomain.GetEnrollById(int.Parse(id));
        if (enrollData == null)
            return new GeneralResponse(false, "Enroll not found", 404);
        enrollData = EnrollMapping.ToModel(enrollmentDto, enrollData);
        var result = await _enrollServiceDomain.UpdateEnroll(enrollData);
        return result;
    }

    public async Task<GeneralResponse> DeleteEnrollment(string enrollId)
    {
        if (string.IsNullOrEmpty(enrollId))
            return new GeneralResponse(false, "Enroll Id is required", 400);
        var enrollData = await _enrollServiceDomain.GetEnrollById(int.Parse(enrollId));
        if (enrollData == null)
            return new GeneralResponse(false, "Enroll not found", 404);
        var result = await _enrollServiceDomain.DeleteEnroll(int.Parse(enrollId));
        return result;
    }

    public async Task<EnrollmentDto?> GetEnrollmentById(string enrollId)
    {
        var enroll = await _enrollServiceDomain.GetEnrollById(int.Parse(enrollId));
        if (enroll == null)
            return null;
        var enrollmentDto = EnrollMapping.ToDto(enroll);
        return enrollmentDto;
    }

    public async Task<GeneralResponse<IEnumerable<EnrollmentDto>?>> GetEnrollmentsByUserId(string userId)
    {
        var user = await _userExternalService.GetDataByUserIdAsync(userId);
        if (user == null)
            return new GeneralResponse<IEnumerable<EnrollmentDto>?>(false, Messages.UserNotFound, 404, null);
        var enroll = await _enrollServiceDomain.GetEnrollsByUserId(int.Parse(userId));
        if (enroll == null)
            return new GeneralResponse<IEnumerable<EnrollmentDto>?>(false, "Enrollments not found", 404, null);
        var enrollmentsDto = enroll.Select(EnrollMapping.ToDto);
        return new GeneralResponse<IEnumerable<EnrollmentDto>?>(true, "Enrollments found", 200, enrollmentsDto);
    }

    public async Task<GeneralResponse<IEnumerable<EnrollmentDto>?>> GetEnrollmentByUserIdAndSchoolId(string userId, string schoolId)
    {
        var user = await _userExternalService.GetDataByUserIdAsync(userId);
        if (user == null)
            return new GeneralResponse<IEnumerable<EnrollmentDto>?>(false, Messages.UserNotFound, 404, null);
        var school = await _schoolExternalService.GetNameBySchoolIdAsync(schoolId);
        if (school == null)
            return new GeneralResponse<IEnumerable<EnrollmentDto>?>(false, Messages.SchoolNotFound, 404, null);
        var enroll = await _enrollServiceDomain.GetEnrollByUserIdAndSchoolId(int.Parse(userId), int.Parse(schoolId));
        if (enroll == null)
            return new GeneralResponse<IEnumerable<EnrollmentDto>?>(false, "Enrollments not found", 404, null);
        var enrollmentsDto = enroll.Select(EnrollMapping.ToDto);
        return new GeneralResponse<IEnumerable<EnrollmentDto>?>(true, "Enrollments found", 200, enrollmentsDto);
    }

    public async Task<GeneralResponse<IEnumerable<EnrollmentDto>?>> GetEnrollmentsBySchoolId(string schoolId)
    {
        var school = await _schoolExternalService.GetNameBySchoolIdAsync(schoolId);
        if (school == null)
            return new GeneralResponse<IEnumerable<EnrollmentDto>?>(false, Messages.SchoolNotFound, 404, null);
        var enrolls = await _enrollServiceDomain.GetEnrollsBySchoolId(int.Parse(schoolId));
        if (enrolls == null)
            return new GeneralResponse<IEnumerable<EnrollmentDto>?>(false, "Enrollments not found", 404, null);
        var enrollmentDtos = enrolls.Select(EnrollMapping.ToDto);
        return new GeneralResponse<IEnumerable<EnrollmentDto>?>(true, "Enrollments found", 200, enrollmentDtos);
    }

    public async Task<GeneralResponse<IEnumerable<CourseExternalDto>?>> GetAvailableCourses(string userId, string schoolId)
    {
        var user = await _userExternalService.GetDataByUserIdAsync(userId);
        if (user == null)
            return new GeneralResponse<IEnumerable<CourseExternalDto>?>(false, Messages.UserNotFound, 404, null);
        var school = await _schoolExternalService.GetCurriculumByIdAsync(schoolId);
        if (school == null || school.Curriculum == null)
            return new GeneralResponse<IEnumerable<CourseExternalDto>?>(false, "School not found or syllabus missing", 404, null);
        var completedCourses = user.Courses ?? new List<string>();
        var completedCoursesSet = new HashSet<string>(completedCourses);
        var availableCourseIds = school.Curriculum
            .Where(c => (c.PreRequeriments ?? new List<string>()).TrueForAll(preReq => completedCoursesSet.Contains(preReq)))
            .Select(c => c.Id)
            .ToList();
        if (availableCourseIds.Count == 0)
            return new GeneralResponse<IEnumerable<CourseExternalDto>?>(false, "No courses available", 404, null);
        var availableCourseIdsFiltered = availableCourseIds
            .Where(courseId => courseId != null && !completedCoursesSet.Contains(courseId))
            .ToList();
        if (availableCourseIdsFiltered.Count == 0)
            return new GeneralResponse<IEnumerable<CourseExternalDto>?>(false, "No courses available", 404, null);
        var courseDtos = new List<CourseExternalDto>();
        foreach (var courseId in availableCourseIdsFiltered)
        {
            if (courseId != null)
            {
                var course = await _coursesExternalService.GetScheduleOfCourseByIdAsync(courseId, schoolId);
                if (course != null)
                    courseDtos.Add(course);
                else
                    return new GeneralResponse<IEnumerable<CourseExternalDto>?>(false, "Course not found", 404, null);
            }
        }
        return new GeneralResponse<IEnumerable<CourseExternalDto>?>(true, "Courses available", 200, courseDtos);
    }
}