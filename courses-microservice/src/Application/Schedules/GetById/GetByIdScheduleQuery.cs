using MediatR;
using ErrorOr;
using Application.Schedules.Common;
namespace Application.Schedules.GetById;

public record GetByIdScheduleQuery(Guid Id) : IRequest<ErrorOr<ScheduleResponse>>;