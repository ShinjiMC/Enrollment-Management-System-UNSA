using Domain.Schedules;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Schedules.Delete;

internal sealed class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, ErrorOr<Unit>>
{
    private readonly IScheduleRepository _ScheduleRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteScheduleCommandHandler(IScheduleRepository ScheduleRepository, IUnitOfWork unitOfWork)
    {
        _ScheduleRepository = ScheduleRepository ?? throw new ArgumentNullException(nameof(ScheduleRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteScheduleCommand command, CancellationToken cancellationToken)
    {
        if (await _ScheduleRepository.GetByIdAsync(command.Id) is not Schedule Schedule)
        {
            return Error.NotFound("Schedule.NotFound", "The Schedule with the provide Id was not found.");
        }
        _ScheduleRepository.Delete(Schedule);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}