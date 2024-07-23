using Domain.Schedules;
using Domain.Primitives;
using MediatR;
using ErrorOr;
using System;
using Domain.DomainErrors;
using Domain.Courses;
using Domain.Schedules.ValueObjects;
using Domain.Schedules.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.Create
{
    internal sealed class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ErrorOr<Unit>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ICourseRepository _courseRepository; // Añadido para obtener el curso
        private readonly IScheduleDomainService _scheduleDomainService; // Añadido para validación de horarios
        private readonly IUnitOfWork _unitOfWork;

        public CreateScheduleCommandHandler(
            IScheduleRepository scheduleRepository, 
            ICourseRepository courseRepository,
            IScheduleDomainService scheduleDomainService,
            IUnitOfWork unitOfWork)
        {
            _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _scheduleDomainService = scheduleDomainService ?? throw new ArgumentNullException(nameof(scheduleDomainService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateScheduleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Convertir DTOs en objetos de dominio
                var scheduleDetails = ScheduleDetailsFactory.Create(command.Details.Group, command.Details.ProfessorId);
                if (scheduleDetails is null)
                {
                    return Errors.Schedule.InvalidScheduleDetails;
                }

                var scheduleEntries = new List<ScheduleEntry>();
                foreach (var entryDto in command.Entries)
                {
                    var scheduleEntry = ScheduleEntryFactory.Create(entryDto.DayOfWeek, entryDto.StartTime, entryDto.EndTime);
                    if (scheduleEntry is null)
                    {
                        return Errors.Schedule.InvalidScheduleHours;
                    }
                    scheduleEntries.Add(scheduleEntry);
                }

                // Obtener el curso asociado
                var courseId = new Guid(command.CourseId);
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course is null)
                {
                    return Error.NotFound("Course.NotFound", "The course with the provided Id was not found.");
                }

                // Validar las entradas del horario
                var isValid = _scheduleDomainService.ValidateScheduleEntries(scheduleEntries, course);
                if (!isValid)
                {
                    return Errors.Schedule.InvalidScheduleHours;
                }

                var schedule = new Schedule(
                    Guid.NewGuid(), // O usa el ScheduleId proporcionado si es necesario
                    command.Year,
                    command.SchoolId,
                    courseId,
                    scheduleEntries,
                    scheduleDetails
                );

                _scheduleRepository.Add(schedule);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("Schedule.Create.Failure", ex.Message);
            }
        }
    }
}
