using ErrorOr;
using MediatR;

namespace Application.Schedules.Update
{
    public record UpdateScheduleCommand(
        Guid ScheduleId,
        Guid CourseId,
        int Year,
        string SchoolId,
        string Group,
        string ProfessorId,
        List<UpdateScheduleEntryDto> Entries
    ) : IRequest<ErrorOr<Unit>>;

    public record UpdateScheduleEntryDto(
        DayOfWeek Day,
        TimeSpan StartTime,
        TimeSpan EndTime
    );
}
