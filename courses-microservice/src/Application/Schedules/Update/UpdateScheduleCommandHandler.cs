using Application.Schedules.Common;
using Domain.Courses;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Schedules.Update;

internal sealed class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ErrorOr<Unit>>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateScheduleCommandHandler(IScheduleRepository scheduleRepository, IUnitOfWork unitOfWork)
    {
        _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateScheduleCommand command, CancellationToken cancellationToken)
    {
        var scheduleId = new ScheduleId(command.ScheduleId);
        var existingSchedule = await _scheduleRepository.GetByIdAsync(scheduleId);

        if (existingSchedule is null)
        {
            return Error.NotFound("Schedule.NotFound", "The Schedule with the provided Id was not found.");
        }

        var scheduleDetails = ScheduleDetails.Create(command.Group, command.ProfessorId);
        if (scheduleDetails is null)
        {
            return Errors.Schedule.InvalidScheduleDetails;
        }

        var updatedEntries = command.Entries.Select(entry => new ScheduleEntry(entry.Day, entry.StartTime, entry.EndTime)).ToList();

        existingSchedule.Update(
            new CourseId(command.CourseId),
            command.Year,
            command.SchoolId,
            scheduleDetails,
            updatedEntries
        );

        _scheduleRepository.Update(existingSchedule);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
