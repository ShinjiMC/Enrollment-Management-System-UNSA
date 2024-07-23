using Application.Schedules.Common;
using Domain.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Schedules.GetById;

internal sealed class GetScheduleByIdQueryHandler : IRequestHandler<GetByIdScheduleQuery, ErrorOr<ScheduleResponse>>
{
    private readonly IScheduleRepository _scheduleRepository;

    public GetScheduleByIdQueryHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
    }

    public async Task<ErrorOr<ScheduleResponse>> Handle(GetByIdScheduleQuery query, CancellationToken cancellationToken)
    {
        if (await _scheduleRepository.GetByIdAsync(new ScheduleId(query.Id)) is not Schedule schedule)
        {
            return Error.NotFound("Schedule.NotFound", "The Schedule with the provided Id was not found.");
        }

        var scheduleResponse = new ScheduleResponse(
            schedule.ScheduleId.Value.ToString(),
            schedule.CourseId.Value.ToString(),
            schedule.SchoolId,
            schedule.Year,
            new ScheduleDetailsDto(
                schedule.ScheduleDetails.Group,
                schedule.ScheduleDetails.ProfessorId
            ),
            schedule.Entries.Select(entry => new ScheduleEntryDto(
                entry.DayOfWeek,
                entry.StartTime,
                entry.EndTime
            )).ToList(), new CourseDto(schedule.Course.Name, schedule.Course.Credits, schedule.Course.Hours, schedule.Course.Active,
                schedule.Course.Semester.Value, schedule.Course.SchoolId.ToString())
        );

        return scheduleResponse;
    }
}
