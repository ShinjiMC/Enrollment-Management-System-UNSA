using course_microservice.context;
using course_microservice.models;
using course_microservice.repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace course_microservice.test.repositories
{
    [TestFixture]
    public class WeekDayRepositoryTest
    {

        private Mock<MyDbContext> _mockContext;
        private WeekDayRepository _repository;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<MyDbContext>();
        }

        [Test]
        public async Task GetAllWeekDays_ShouldReturnAllWeekDays()
        {
            // Arrange
            IQueryable<WeekDayModel> weekDays = new List<WeekDayModel>
            {
                new WeekDayModel { ID = 1, Name = "Monday" },
                new WeekDayModel { ID = 2, Name = "Tuesday" },
                new WeekDayModel { ID = 3, Name = "Wednesday" }
            }.AsQueryable();

            var dbSetMock = MockDbSetHelper.CreateDbSetMock(weekDays);


            _mockContext.Setup(c => c.WeekDay).Returns(dbSetMock.Object);


            WeekDayRepository repository = new WeekDayRepository(_mockContext.Object);

            // Act
            List<WeekDayModel> result = await repository.GetAllWeekDays();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Monday", result[0].Name);
            Assert.AreEqual("Tuesday", result[1].Name);
            Assert.AreEqual("Wednesday", result[2].Name);
        }

        [Test]
        public async Task GetWeekDay_ShouldReturnCorrectWeekDayById()
        {
            // Arrange
            int weekDayId = 1;
            WeekDayModel expectedWeekDay = new WeekDayModel { ID = weekDayId, Name = "Monday" };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.WeekDay.FindAsync(weekDayId)).ReturnsAsync(expectedWeekDay);

            WeekDayRepository repository = new WeekDayRepository(mockContext.Object);

            // Act
            WeekDayModel result = await repository.GetWeekDay(weekDayId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(weekDayId, result.ID);
            Assert.AreEqual("Monday", result.Name);
        }

        [Test]
        public async Task AddWeekDay_ShouldAddWeekDay()
        {
            // Arrange
            WeekDayModel weekDayToAdd = new WeekDayModel { ID = 1, Name = "Monday" };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.WeekDay.Add(weekDayToAdd));

            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            WeekDayRepository repository = new WeekDayRepository(mockContext.Object);

            // Act
            WeekDayModel result = await repository.AddWeekDay(weekDayToAdd);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.ID);
            Assert.AreEqual("Monday", result.Name);
        }

        [Test]
        public async Task UpdateWeekDay_ShouldUpdateWeekDay()
        {
            // Arrange
            int weekDayId = 1;
            WeekDayModel existingWeekDay = new WeekDayModel { ID = weekDayId, Name = "Monday" };
            WeekDayModel updatedWeekDay = new WeekDayModel { ID = weekDayId, Name = "Updated Monday" };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.WeekDay.FindAsync(weekDayId)).ReturnsAsync(existingWeekDay);

            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            WeekDayRepository repository = new WeekDayRepository(mockContext.Object);

            // Act
            WeekDayModel result = await repository.UpdateWeekDay(weekDayId, updatedWeekDay);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(weekDayId, result.ID);
            Assert.AreEqual("Updated Monday", result.Name);
        }

        [Test]
        public async Task DeleteWeekDay_ShouldDeleteWeekDay()
        {
            // Arrange
            int weekDayId = 1;
            WeekDayModel existingWeekDay = new WeekDayModel { ID = weekDayId, Name = "Monday" };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.WeekDay.FindAsync(weekDayId)).ReturnsAsync(existingWeekDay);
            mockContext.Setup(c => c.WeekDay.Remove(existingWeekDay));
            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            WeekDayRepository repository = new WeekDayRepository(mockContext.Object);

            // Act
            bool result = await repository.DeleteWeekDay(weekDayId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteWeekDay_ShouldReturnFalseWhenScheduleNotFound()
        { 
            // Arrange
            int weekDayId = 1;
            
            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.WeekDay.FindAsync(weekDayId)).ReturnsAsync((WeekDayModel)null);

            WeekDayRepository repository = new WeekDayRepository(mockContext.Object);

            // Act
            bool result = await repository.DeleteWeekDay(weekDayId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
