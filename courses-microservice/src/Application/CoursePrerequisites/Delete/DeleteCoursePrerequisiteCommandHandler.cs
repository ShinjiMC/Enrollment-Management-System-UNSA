using Application.CoursesPrerequisite.Delete;
using Domain.Courses;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Courses.Delete;

internal sealed class DeleteCoursePrerequisiteCommandHandler : IRequestHandler<DeleteCoursePrerequisiteCommand, ErrorOr<Unit>>
{
    private readonly ICoursePrerequisiteRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCoursePrerequisiteCommandHandler(ICoursePrerequisiteRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteCoursePrerequisiteCommand command, CancellationToken cancellationToken)
    {
        var coursePrerequisite = await _courseRepository.GetByIdAsync(command.CourseId, command.CoursePrerequisiteId);

        if (coursePrerequisite == null)
        {
            return Error.NotFound("CoursePrerequisite.NotFound", "The course prerequisite with the provided IDs was not found.");
        }

        await _courseRepository.DeleteAsync(command.CourseId, command.CoursePrerequisiteId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}