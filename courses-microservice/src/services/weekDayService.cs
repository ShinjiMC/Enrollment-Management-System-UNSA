using course_microservice.models;
using course_microservice.repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace course_microservice.services
{
    public interface IWeekDayService
    {
        Task<List<WeekDayModel>> GetAllWeekDays();
        Task<WeekDayModel> GetWeekDay(int id);
        Task<WeekDayModel> AddWeekDay(WeekDayModel weekDay);
        Task<WeekDayModel> UpdateWeekDay(int id, WeekDayModel weekDay);
        Task<bool> DeleteWeekDay(int id);
    }

    public class WeekDayService : IWeekDayService
    {
        private readonly IWeekDayRepository _weekDayRepository;

        public WeekDayService(IWeekDayRepository weekDayRepository)
        {
            _weekDayRepository = weekDayRepository;
        }

        public Task<List<WeekDayModel>> GetAllWeekDays()
        {
            return _weekDayRepository.GetAllWeekDays();
        }

        public Task<WeekDayModel> GetWeekDay(int id)
        {
            return _weekDayRepository.GetWeekDay(id);
        }

        public Task<WeekDayModel> AddWeekDay(WeekDayModel weekDay)
        {
            return _weekDayRepository.AddWeekDay(weekDay);
        }

        public Task<WeekDayModel> UpdateWeekDay(int id, WeekDayModel weekDay)
        {
            return _weekDayRepository.UpdateWeekDay(id, weekDay);
        }

        public Task<bool> DeleteWeekDay(int id)
        {
            return _weekDayRepository.DeleteWeekDay(id);
        }
    }
}
