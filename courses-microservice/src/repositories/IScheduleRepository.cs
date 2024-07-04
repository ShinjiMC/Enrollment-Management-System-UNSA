using System.Collections.Generic;
using System.Threading.Tasks;
using course_microservice.context;
using course_microservice.DTOs;
using course_microservice.models;
using Microsoft.EntityFrameworkCore;

namespace course_microservice.repositories
{
    public interface IScheduleRepository
    {
        Task<List<ScheduleModel>> GetAllSchedules();
        Task<ScheduleModel> GetSchedule(int id);
        Task<ScheduleModel> AddSchedule(ScheduleModel schedule);
        Task<ScheduleModel> UpdateSchedule(int id, ScheduleModel updatedSchedule);
        Task<bool> DeleteSchedule(int id);

        Task<List<ScheduleModel>> GetSchedulesByYearSemesterSchool(int year, char semester, int schoolID);

    }

    public class ScheduleRepository : IScheduleRepository
    {
        private readonly MyDbContext _dbContext;

        public ScheduleRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ScheduleModel>> GetAllSchedules()
        {
            return await _dbContext.Schedule.Include(s => s.Course).Include(s => s.WeekDay).Include(s => s.School).ToListAsync();
        }

        public async Task<ScheduleModel> GetSchedule(int id)
        {
            return await _dbContext.Schedule.FindAsync(id);
        }

        public async Task<ScheduleModel> AddSchedule(ScheduleModel schedule)
        {
            _dbContext.Schedule.Add(schedule);
            await _dbContext.SaveChangesAsync();
            return schedule;
        }

        public async Task<ScheduleModel> UpdateSchedule(int id, ScheduleModel updatedSchedule)
        {
            var schedule = await _dbContext.Schedule.FindAsync(id);
            if (schedule != null)
            {
                schedule.CourseID = updatedSchedule.CourseID;
                schedule.WeekDayID = updatedSchedule.WeekDayID;
                schedule.SchoolID = updatedSchedule.SchoolID;
                schedule.Group = updatedSchedule.Group;
                schedule.Year = updatedSchedule.Year;
                schedule.StartTime = updatedSchedule.StartTime;
                schedule.EndTime = updatedSchedule.EndTime;
                schedule.TeacherFullName = updatedSchedule.TeacherFullName;
                schedule.Capacity = updatedSchedule.Capacity;
                await _dbContext.SaveChangesAsync();
            }
            return schedule;
        }

        public async Task<bool> DeleteSchedule(int id)
        {
            var schedule = await _dbContext.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _dbContext.Schedule.Remove(schedule);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ScheduleModel>> GetSchedulesByYearSemesterSchool(int year, char semester, int schoolID)
        {
            return await _dbContext.Schedule
                .Include(s => s.Course)
                .Include(s => s.WeekDay)
                .Include(s => s.School)
                .Where(s => s.Year == year && s.Course.Semester == semester && s.SchoolID == schoolID)
                .ToListAsync();
        }
    }
}
