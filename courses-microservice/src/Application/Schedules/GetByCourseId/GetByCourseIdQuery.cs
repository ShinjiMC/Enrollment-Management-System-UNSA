using MediatR;
using ErrorOr;
using Application.Schedules.Common;
namespace Application.Schedules.GetByCourseIdQuery;

public record GetByCourseIdQuery(Guid Id) : IRequest<ErrorOr<IReadOnlyList<ScheduleResponse>>>;   