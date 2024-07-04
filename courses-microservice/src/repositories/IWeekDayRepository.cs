using course_microservice.context;
using course_microservice.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace course_microservice.repositories
{
    public interface IWeekDayRepository
    {
        Task<List<WeekDayModel>> GetAllWeekDays();
        Task<WeekDayModel> GetWeekDay(int id);
        Task<WeekDayModel> AddWeekDay(WeekDayModel weekDay);
        Task<WeekDayModel> UpdateWeekDay(int id, WeekDayModel updatedWeekDay);
        Task<bool> DeleteWeekDay(int id);
    }

    public class WeekDayRepository : IWeekDayRepository
    {
        private readonly MyDbContext _dbContext;

        public WeekDayRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WeekDayModel>> GetAllWeekDays()
        {
            return await _dbContext.WeekDay.ToListAsync();
        }

        public async Task<WeekDayModel> GetWeekDay(int id)
        {
            return await _dbContext.WeekDay.FindAsync(id);
        }

        public async Task<WeekDayModel> AddWeekDay(WeekDayModel weekDay)
        {
            var existingWeekDay = await _dbContext.WeekDay.FindAsync(weekDay.ID);
            if (existingWeekDay != null)
            {
                return null;
            }

            _dbContext.WeekDay.Add(weekDay);
            await _dbContext.SaveChangesAsync();
            return weekDay;
        }

        public async Task<WeekDayModel> UpdateWeekDay(int id, WeekDayModel updatedWeekDay)
        {
            var weekDay = await _dbContext.WeekDay.FindAsync(id);
            if (weekDay != null)
            {
                weekDay.Name = updatedWeekDay.Name;
                await _dbContext.SaveChangesAsync();
            }
            return weekDay;
        }

        public async Task<bool> DeleteWeekDay(int id)
        {
            var weekDay = await _dbContext.WeekDay.FindAsync(id);
            if (weekDay != null)
            {
                _dbContext.WeekDay.Remove(weekDay);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}