using MediatR;
using ErrorOr;
using Application.Schedules.Common;
using Courses.Common;
namespace Application.Courses.GetById;

public record GetByIdCoursesQuery(Guid Id) : IRequest<ErrorOr<CourseResponse>>;   