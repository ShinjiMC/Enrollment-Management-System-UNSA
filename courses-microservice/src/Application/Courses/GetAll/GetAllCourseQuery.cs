using MediatR;
using ErrorOr;
using Courses.Common;
namespace Application.Courses.GetAll;

public record GetAllCoursesQuery() : IRequest<ErrorOr<IReadOnlyList<CourseResponse>>>;   