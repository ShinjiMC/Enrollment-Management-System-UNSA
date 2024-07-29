using Domain.Schedules;
using MediatR;
using ErrorOr;
using Application.Schedules.Common;

namespace Application.Schedules.GetByCourseIdQuery;

internal sealed class GetAllScheduleQueryHandler : IRequestHandler<GetByCourseIdQuery, ErrorOr<IReadOnlyList<ScheduleResponse>>>
{
    private readonly IScheduleRepository _scheduleRepository;

    public GetAllScheduleQueryHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<ScheduleResponse>>> Handle(GetByCourseIdQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Schedule> schedules = await _scheduleRepository.GetByCourseIdAsync(query.Id);

        var response = schedules.Select(schedule => new ScheduleResponse(
                schedule.ScheduleId.ToString(),
                schedule.CourseId.ToString(),
                schedule.SchoolId,
                schedule.Year,
                new ScheduleDetailsDto(schedule.ScheduleDetails.Group, schedule.ScheduleDetails.ProfessorId),
                schedule.Entries.Select(entry => new ScheduleEntryDto(entry.DayOfWeek, entry.StartTime, entry.EndTime)),
                new CourseDto(schedule.Course.Name, schedule.Course.Credits, schedule.Course.Hours, schedule.Course.Active, 
                schedule.Course.Semester.Value ,schedule.Course.SchoolId.ToString())
        )).ToList();

        return response;
    }
}