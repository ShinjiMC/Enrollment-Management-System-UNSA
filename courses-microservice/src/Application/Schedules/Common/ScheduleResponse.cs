using MediatR;
using ErrorOr;
using Domain.Schedules;
using Domain.Courses;

namespace Application.Schedules.Common
{
    public record ScheduleResponse(
        string ScheduleId,
        string CourseId,
        string SchoolId,
        int Year,
        ScheduleDetailsDto Details,
        IEnumerable<ScheduleEntryDto> Entries,
        CourseDto Course // Información del curso
    ) : IRequest<ErrorOr<Unit>>;

    public record ScheduleDetailsDto(
        string Group,
        string ProfessorId
    );

    public record ScheduleEntryDto(
        DayOfWeek Day,
        TimeSpan StartTime,
        TimeSpan EndTime
    );

    public record CourseDto
    (   string Name,
        int Credits,
        int Hours,
        bool Active,
        string Semester,
        string SchoolId
    );
}
