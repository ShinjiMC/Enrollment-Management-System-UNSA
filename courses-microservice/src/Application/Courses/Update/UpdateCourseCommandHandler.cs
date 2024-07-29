using Domain.Courses;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Schedules;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses.Update;

internal sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ErrorOr<Unit>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        // Validar semestre
        if (SemesterFactory.Create(command.Semester) is not Semester semester)
        {
            return Errors.Course.InvalidSemester;
        }

        if (command.Hours == 0)
        {
            return Errors.Course.InvalidHours;
        }
        if (command.Credits == 0)
        {
            return Errors.Course.InvalidCredits;
        }

        // Verificar si el curso existe
        var existingCourse = await _courseRepository.GetByIdAsync(command.Id);
        if (existingCourse is null)
        {
            return Error.NotFound("Course.NotFound", "The Course with the provide Id was not found.");
        }

        // Actualizar el curso
        Course course = Course.UpdateCourse(
            command.Id,
            command.Name,
            command.Credits,
            command.Hours,
            command.Active,
            semester,
            command.SchoolId);

        await _courseRepository.Update(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
