using enrollments_microservice.Application.Dtos;
using enrollments_microservice.Domain.Entities;
using enrollments_microservice.Domain.ValueObjects;

namespace enrollments_microservice.Application.Mapping;

public static class EnrollMapping
{
    public static EnrollmentDto ToDto(this EnrollModel enrollment)
    {
        if (enrollment == null) throw new ArgumentNullException(nameof(enrollment));
        return new EnrollmentDto
        {
            Id = enrollment.Id.ToString(),
            StudentId = enrollment.Student?.StudentID.ToString() ?? string.Empty,
            FullName = enrollment.Student?.FullName.ToString() ?? string.Empty,
            AcademicPerformance = enrollment.Student?.AcademicPerformance ?? 0,
            Credits = enrollment.Student?.Credits ?? 0,
            Courses = enrollment.Courses?.Select(c => new CourseDto
            {
                Id = c.CourseId?.ToString() ?? string.Empty,
                Group = c.Group ?? string.Empty
            }).ToList() ?? new List<CourseDto>(),
            SchoolId = enrollment.School?.SchoolID.ToString() ?? string.Empty,
            SchoolName = enrollment.School?.Name ?? string.Empty
        };
    }

    public static EnrollModel ToModel(this EnrollmentDto enrollment)
    {
        if (enrollment == null) throw new ArgumentNullException(nameof(enrollment));

        var ValueObjectStudentData = new StudentData(
            studentId: int.TryParse(enrollment.StudentId, out var studentId) ? studentId : 0,
            fullName: enrollment.FullName == null ? string.Empty : enrollment.FullName,
            academicPerformance: enrollment.AcademicPerformance ?? enrollment.AcademicPerformance.GetValueOrDefault(),
            credits: enrollment.Credits ?? 0
        );

        var ValueObjectSchoolData = new SchoolData(
            id: int.TryParse(enrollment.SchoolId, out var schoolId) ? schoolId : 0,
            name: enrollment.SchoolName == null ? string.Empty : enrollment.SchoolName
        );
        return new EnrollModel
        {
            School = ValueObjectSchoolData,
            Student = ValueObjectStudentData,
            Courses = enrollment.Courses?.Select(static c => new CourseModel
            {
                CourseId = c.Id,
                Group = c.Group
            }).ToList() ?? new List<CourseModel>(),
        };
    }

    public static EnrollModel ToModel(this EnrollmentDto enrollmentDto, EnrollModel existingEnroll)
    {
        if (enrollmentDto == null || existingEnroll == null) throw new ArgumentNullException(nameof(enrollmentDto));
        var ValueObjectStudentData = new StudentData(
            studentId: int.TryParse(enrollmentDto.StudentId, out var studentId) ? studentId : existingEnroll.Student?.StudentID ?? 0,
            fullName: enrollmentDto.FullName == null ? existingEnroll.Student?.FullName ?? string.Empty : enrollmentDto.FullName,
            academicPerformance: enrollmentDto.AcademicPerformance ?? existingEnroll.Student?.AcademicPerformance ?? 0,
            credits: enrollmentDto.Credits ?? existingEnroll.Student?.Credits ?? 0
        );

        var ValueObjectSchoolData = new SchoolData(
            id: int.TryParse(enrollmentDto.SchoolId, out var schoolId) ? schoolId : existingEnroll.School?.SchoolID ?? 0,
            name: enrollmentDto.SchoolName == null ? existingEnroll.School?.Name ?? string.Empty : enrollmentDto.SchoolName
        );

        return new EnrollModel
        {
            Id = existingEnroll.Id,
            School = ValueObjectSchoolData,
            Student = ValueObjectStudentData,
            Courses = enrollmentDto.Courses?.Select(static c => new CourseModel
            {
                CourseId = c.Id,
                Group = c.Group
            }).ToList() ?? existingEnroll.Courses?.Select(static c => new CourseModel
            {
                CourseId = c.CourseId,
                Group = c.Group
            }).ToList() ?? new List<CourseModel>(),
        };
    }
}