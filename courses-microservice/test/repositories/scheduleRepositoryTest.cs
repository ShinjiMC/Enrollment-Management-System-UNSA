using course_microservice.context;
using course_microservice.models;
using course_microservice.repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace course_microservice.test.repositories
{
    [TestFixture]
    public class ScheduleRepositoryTest
    {
        [Test]
        public async Task GetAllSchedules_ShouldReturnAllSchedules()
        {
            // Arrange
            IQueryable<ScheduleModel> schedules = new List<ScheduleModel>
            {
                new ScheduleModel { ID = 1, CourseID = 1, WeekDayID = 1, SchoolID = 1, Group = "Group A", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "John Doe", Capacity = 30 },
                new ScheduleModel { ID = 2, CourseID = 2, WeekDayID = 2, SchoolID = 1, Group = "Group B", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "Jane Smith", Capacity = 25 }
            }.AsQueryable();

            var _mockContext = new Mock<MyDbContext>();
            var dbSetMock = MockDbSetHelper.CreateDbSetMock(schedules);

            _mockContext.Setup(c => c.Schedule).Returns(dbSetMock.Object);

            ScheduleRepository repository = new ScheduleRepository(_mockContext.Object);

            // Act
            List<ScheduleModel> result = await repository.GetAllSchedules();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Group A", result[0].Group);
            Assert.AreEqual("Group B", result[1].Group);
        }

        [Test]
        public async Task GetSchedule_ShouldReturnCorrectScheduleById()
        {
            // Arrange
            int scheduleId = 1;
            ScheduleModel expectedSchedule = new ScheduleModel { ID = scheduleId, CourseID = 1, WeekDayID = 1, SchoolID = 1, Group = "Group A", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "John Doe", Capacity = 30 };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.Schedule.FindAsync(scheduleId)).ReturnsAsync(expectedSchedule);

            ScheduleRepository repository = new ScheduleRepository(mockContext.Object);

            // Act
            ScheduleModel result = await repository.GetSchedule(scheduleId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(scheduleId, result.ID);
            Assert.AreEqual("Group A", result.Group);
        }

        [Test]
        public async Task AddSchedule_ShouldAddSchedule()
        {
            // Arrange
            ScheduleModel scheduleToAdd = new ScheduleModel { ID = 1, CourseID = 1, WeekDayID = 1, SchoolID = 1, Group = "Group A", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "John Doe", Capacity = 30 };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.Schedule.Add(scheduleToAdd));

            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            ScheduleRepository repository = new ScheduleRepository(mockContext.Object);

            // Act
            ScheduleModel result = await repository.AddSchedule(scheduleToAdd);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.ID);
            Assert.AreEqual("Group A", result.Group);
        }

        [Test]
        public async Task UpdateSchedule_ShouldUpdateSchedule()
        {
            // Arrange
            int scheduleId = 1;
            ScheduleModel existingSchedule = new ScheduleModel { ID = scheduleId, CourseID = 1, WeekDayID = 1, SchoolID = 1, Group = "Group A", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "John Doe", Capacity = 30 };
            ScheduleModel updatedSchedule = new ScheduleModel { ID = scheduleId, CourseID = 2, WeekDayID = 2, SchoolID = 1, Group = "Group B", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "Jane Smith", Capacity = 25 };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.Schedule.FindAsync(scheduleId)).ReturnsAsync(existingSchedule);

            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            ScheduleRepository repository = new ScheduleRepository(mockContext.Object);

            // Act
            ScheduleModel result = await repository.UpdateSchedule(scheduleId, updatedSchedule);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(scheduleId, result.ID);
            Assert.AreEqual("Group B", result.Group);
        }

        [Test]
        public async Task DeleteSchedule_ShouldDeleteSchedule()
        {
            // Arrange
            int scheduleId = 1;
            ScheduleModel existingSchedule = new ScheduleModel { ID = scheduleId, CourseID = 1, WeekDayID = 1, SchoolID = 1, Group = "Group A", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "John Doe", Capacity = 30 };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.Schedule.FindAsync(scheduleId)).ReturnsAsync(existingSchedule);
            mockContext.Setup(c => c.Schedule.Remove(existingSchedule));
            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            ScheduleRepository repository = new ScheduleRepository(mockContext.Object);

            // Act
            bool result = await repository.DeleteSchedule(scheduleId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteSchedule_ShouldReturnFalseWhenScheduleNotFound()
        {
            // Arrange
            int scheduleId = 1;
            
            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.Schedule.FindAsync(scheduleId)).ReturnsAsync((ScheduleModel)null);

            ScheduleRepository repository = new ScheduleRepository(mockContext.Object);

            // Act
            bool result = await repository.DeleteSchedule(scheduleId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetSchedulesByYearSemesterSchool_ShouldReturnSchedules()
        {
            // Arrange
            int year = 2023;
            char semester = 'A';
            int schoolId = 1;

            IQueryable<ScheduleModel> schedules = new List<ScheduleModel>
            {
                new ScheduleModel { ID = 1, CourseID = 1, WeekDayID = 1, SchoolID = 1, Group = "Group A", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "John Doe", Capacity = 30, Course = new CourseModel {Semester = 'A'} },
                new ScheduleModel { ID = 2, CourseID = 2, WeekDayID = 2, SchoolID = 1, Group = "Group B", Year = 2023, StartTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), TeacherFullName = "Jane Smith", Capacity = 25, Course = new CourseModel {Semester = 'A'}}
            }.AsQueryable();


            var _mockContext = new Mock<MyDbContext>();

            var dbSetMock = MockDbSetHelper.CreateDbSetMock(schedules);

            _mockContext.Setup(c => c.Schedule).Returns(dbSetMock.Object);

            ScheduleRepository repository = new ScheduleRepository(_mockContext.Object);

            // Act
            List<ScheduleModel> result = await repository.GetSchedulesByYearSemesterSchool(year, semester, schoolId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Group A", result[0].Group);
            Assert.AreEqual("Group B", result[1].Group);
        }
    }
}
