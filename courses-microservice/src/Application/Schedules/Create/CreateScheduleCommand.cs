using MediatR;
using ErrorOr;

namespace Application.Schedules.Create
{
    public record CreateScheduleCommand(
        string CourseId,
        int Year,
        string SchoolId,
        ScheduleDetailsDto Details,
        List<ScheduleEntryDto> Entries
    ) : IRequest<ErrorOr<Unit>>;
    
    public record ScheduleDetailsDto(
        string Group,
        string ProfessorId
    );

    public record ScheduleEntryDto(
        DayOfWeek DayOfWeek,
        TimeSpan StartTime,
        TimeSpan EndTime
    );
}
