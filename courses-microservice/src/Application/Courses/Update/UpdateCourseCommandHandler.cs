using Domain.Courses;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Courses.Update;

internal sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ErrorOr<Unit>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCourseCommandHandler(ICourseRepository CourseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = CourseRepository ?? throw new ArgumentNullException(nameof(CourseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        // Validar semestre
        if (Semester.Create(command.Semester) is not Semester semester)
        {
            return Errors.Course.InvalidSemester;
        }

        Course course = Course.UpdateCourse(
            command.Id, 
            command.Name,
            command.Credits,
            command.Hours,
            command.Active,
            semester,
            command.SchoolId);

        _courseRepository.Update(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}