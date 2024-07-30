using Domain.Courses;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Courses.Delete;

internal sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, ErrorOr<Unit>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        if (await _courseRepository.GetByIdAsync(command.Id) is not Course course)
        {
            return Error.NotFound("Course.NotFound", "The course with the provide Id was not found.");
        }
        _courseRepository.Delete(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}