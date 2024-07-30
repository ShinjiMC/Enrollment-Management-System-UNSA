using MediatR;
using ErrorOr;
using Application.Schedules.Common;

namespace Application.Schedules.Create
{
    public record CreateScheduleCommand(
        string CourseId,
        int Year,
        string SchoolId,
        ScheduleDetailsCreate Details,
        IEnumerable<ScheduleEntryCreate> Entries
    ) : IRequest<ErrorOr<ScheduleResponse>>;
    
    public record ScheduleDetailsCreate(
        string Group,
        string ProfessorId
    );

    public record ScheduleEntryCreate(
        DayOfWeek DayOfWeek,
        TimeSpan StartTime,
        TimeSpan EndTime
    );
}
