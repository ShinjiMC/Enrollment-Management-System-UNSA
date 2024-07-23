using MediatR;
using ErrorOr;
using Application.Schedules.Common;
namespace Application.Schedules.GetAll;

public record GetAllScheduleQuery() : IRequest<ErrorOr<IReadOnlyList<ScheduleResponse>>>;   