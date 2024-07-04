using course_microservice.DTOs;
using course_microservice.models;
using course_microservice.repositories;

namespace course_microservice.services
{
    public interface IScheduleService 
    {
        Task<List<ScheduleModel>> GetAllSchedules();
        Task<ScheduleModel> GetSchedule(int ID);
        Task<ScheduleModel> AddSchedule(ScheduleModel schedule);
        Task<ScheduleModel> UpdateSchedule(int ID, ScheduleModel updatedSchedule);
        Task<bool> DeleteSchedule(int ID);
        Task<List<ScheduleModel>> GetSchedulesByYearSemesterSchool(int year, char semester, int schoolID);
    }

    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IWeekDayRepository _weekDayRepository;
        private readonly ISchoolRepository _schoolRepository;


        public ScheduleService(
            IScheduleRepository scheduleRepository,
            ICourseRepository courseRepository,
            IWeekDayRepository weekDayRepository,
            ISchoolRepository schoolRepository)
        {
            _scheduleRepository = scheduleRepository;
            _courseRepository = courseRepository;
            _weekDayRepository = weekDayRepository;
            _schoolRepository = schoolRepository;
        }

        public async Task<List<ScheduleModel>> GetAllSchedules()
        {
            return await _scheduleRepository.GetAllSchedules();
        }

        public async Task<ScheduleModel> GetSchedule(int ID)
        {
            return await _scheduleRepository.GetSchedule(ID);
        }

        public async Task<ScheduleModel> AddSchedule(ScheduleModel schedule)
        {
            return await _scheduleRepository.AddSchedule(schedule);
        }

        public async Task<ScheduleModel> UpdateSchedule(int ID, ScheduleModel schedule)
        {
            return await _scheduleRepository.UpdateSchedule(ID, schedule);
        }

        public async Task<bool> DeleteSchedule(int ID)
        {
            return await _scheduleRepository.DeleteSchedule(ID);
        }

        public async Task<List<ScheduleModel>> GetSchedulesByYearSemesterSchool(int year, char semester, int schoolID){
            return await _scheduleRepository.GetSchedulesByYearSemesterSchool(year,semester, schoolID);
        }
        
    }
}
