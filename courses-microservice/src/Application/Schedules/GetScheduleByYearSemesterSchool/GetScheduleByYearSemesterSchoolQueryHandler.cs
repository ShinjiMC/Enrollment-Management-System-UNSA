using MediatR;
using Domain.Schedules;
using ErrorOr;
using Application.Schedules.Common;

namespace Application.Schedules.GetByYearSemesterSchool
{
    public class GetScheduleByYearSemesterSchoolQueryHandler : IRequestHandler<GetByYearSemesterSchool, ErrorOr<List<ScheduleResponse>>>
    {
        private readonly IScheduleRepository _scheduleRepository;

        public GetScheduleByYearSemesterSchoolQueryHandler(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ErrorOr<List<ScheduleResponse>>> Handle(GetByYearSemesterSchool request, CancellationToken cancellationToken)
        {
            List<Schedule> schedules = await _scheduleRepository.GetBySemesterYearAndSchoolAsync(request.Year, request.Semester, request.SchoolId);

            var scheduleDtos = schedules.Select(schedule => new ScheduleResponse(
            schedule.ScheduleId.ToString(),
            schedule.CourseId.ToString(),
            schedule.SchoolId,
            schedule.Year,
            new ScheduleDetailsDto(schedule.ScheduleDetails.Group, schedule.ScheduleDetails.ProfessorId),
            schedule.Entries.Select(entry => new ScheduleEntryDto(entry.DayOfWeek, entry.StartTime, entry.EndTime)),
            new CourseDto(schedule.Course.Name, schedule.Course.Credits, schedule.Course.Hours, schedule.Course.Active, 
                schedule.Course.Semester.Value ,schedule.Course.SchoolId.ToString()) // Mapea la información del curso
        )).ToList();

            return scheduleDtos;
        }
    }
}
