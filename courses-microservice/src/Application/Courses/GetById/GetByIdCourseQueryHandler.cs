using Courses.Common;
using Domain.Courses;
using ErrorOr;
using MediatR;

namespace Application.Courses.GetById;


internal sealed class GetCourseByIdQueryHandler : IRequestHandler<GetByIdCoursesQuery, ErrorOr<CourseResponse>>
{
    private readonly ICourseRepository _CourseRepository;

    public GetCourseByIdQueryHandler(ICourseRepository CourseRepository)
    {
        _CourseRepository = CourseRepository ?? throw new ArgumentNullException(nameof(CourseRepository));
    }

    public async Task<ErrorOr<CourseResponse>> Handle(GetByIdCoursesQuery query, CancellationToken cancellationToken)
    {
        if (await _CourseRepository.GetByIdAsync(query.Id) is not Course course)
        {
            return Error.NotFound("Course.NotFound", "The Course with the provide Id was not found.");
        }

        return new CourseResponse(
            course.CourseId, 
            course.Name, 
            course.Credits, 
            course.Hours,
            course.Active,
            course.Semester.Value,
            course.SchoolId);
    }
}