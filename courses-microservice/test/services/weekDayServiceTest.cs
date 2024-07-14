using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using course_microservice.models;
using course_microservice.repositories;
using course_microservice.services;

namespace course_microservice.test.services
{
    [TestFixture]
    public class WeekDayServiceTests
    {
        private Mock<IWeekDayRepository> _mockWeekDayRepository;
        private IWeekDayService _weekDayService;

        [SetUp]
        public void SetUp()
        {
            _mockWeekDayRepository = new Mock<IWeekDayRepository>();
            _weekDayService = new WeekDayService(_mockWeekDayRepository.Object);
        }

        [Test]
        public async Task GetAllWeekDays_ShouldReturnAllWeekDays()
        {
            // Arrange
            var weekDays = new List<WeekDayModel>
            {
                new WeekDayModel { ID = 1, Name = "Monday" },
                new WeekDayModel { ID = 2, Name = "Tuesday" }
            };
            _mockWeekDayRepository.Setup(repo => repo.GetAllWeekDays()).ReturnsAsync(weekDays);

            // Act
            var result = await _weekDayService.GetAllWeekDays();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(weekDays));
        }

        [Test]
        public async Task GetWeekDay_ShouldReturnWeekDayById()
        {
            // Arrange
            var weekDay = new WeekDayModel { ID = 1, Name = "Monday" };
            _mockWeekDayRepository.Setup(repo => repo.GetWeekDay(1)).ReturnsAsync(weekDay);

            // Act
            var result = await _weekDayService.GetWeekDay(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(weekDay));
        }

        [Test]
        public async Task AddWeekDay_ShouldAddWeekDay()
        {
            // Arrange
            var weekDay = new WeekDayModel { ID = 1, Name = "Monday" };
            _mockWeekDayRepository.Setup(repo => repo.AddWeekDay(weekDay)).ReturnsAsync(weekDay);

            // Act
            var result = await _weekDayService.AddWeekDay(weekDay);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(weekDay));
        }

        [Test]
        public async Task UpdateWeekDay_ShouldUpdateWeekDay()
        {
            // Arrange
            var updatedWeekDay = new WeekDayModel { ID = 1, Name = "Updated Monday" };
            _mockWeekDayRepository.Setup(repo => repo.UpdateWeekDay(1, updatedWeekDay)).ReturnsAsync(updatedWeekDay);

            // Act
            var result = await _weekDayService.UpdateWeekDay(1, updatedWeekDay);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(updatedWeekDay));
        }

        [Test]
        public async Task DeleteWeekDay_ShouldReturnTrueIfWeekDayIsDeleted()
        {
            // Arrange
            _mockWeekDayRepository.Setup(repo => repo.DeleteWeekDay(1)).ReturnsAsync(true);

            // Act
            var result = await _weekDayService.DeleteWeekDay(1);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
