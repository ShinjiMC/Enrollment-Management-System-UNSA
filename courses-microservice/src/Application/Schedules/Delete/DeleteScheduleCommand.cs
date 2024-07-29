using ErrorOr;
using MediatR;

namespace Application.Schedules.Delete
{
    public record DeleteScheduleCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
