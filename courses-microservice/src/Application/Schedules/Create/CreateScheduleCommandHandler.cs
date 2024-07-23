using Domain.Schedules;
using Domain.Primitives;
using MediatR;
using ErrorOr;
using System;
using Domain.DomainErrors;
using Domain.Courses;

namespace Application.Schedules.Create
{
    internal sealed class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ErrorOr<Unit>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateScheduleCommandHandler(IScheduleRepository scheduleRepository, IUnitOfWork unitOfWork)
        {
            _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateScheduleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Convert DTOs to domain objects
                var scheduleDetails = ScheduleDetails.Create(command.Details.Group, command.Details.ProfessorId);
                if (scheduleDetails is null)
                {
                    return Errors.Schedule.InvalidScheduleDetails;
                }

                var scheduleEntries = new List<ScheduleEntry>();
                foreach (var entryDto in command.Entries)
                {
                    var scheduleEntry = new ScheduleEntry(entryDto.DayOfWeek, entryDto.StartTime, entryDto.EndTime);
                    scheduleEntries.Add(scheduleEntry);
                }

                var schedule = new Schedule(
                    new ScheduleId(Guid.NewGuid()), // Or use provided ScheduleId if necessary
                    command.Year,
                    command.SchoolId,
                    new CourseId( new Guid(command.CourseId)),
                    scheduleEntries,
                    scheduleDetails
                );

                _scheduleRepository.Add(schedule);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("Schedule.Create.Failure", ex.Message);
            }
        }
    }
}
