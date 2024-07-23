using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using course_microservice.controllers;
using course_microservice.models;
using course_microservice.services;
using course_microservice.DTOs;

namespace course_microservice.tests.controllers
{
    [TestFixture]
    public class WeekDayControllerTests
    {
        private Mock<IWeekDayService> _mockWeekDayService;
        private WeekDayController _weekDayController;

        [SetUp]
        public void SetUp()
        {
            _mockWeekDayService = new Mock<IWeekDayService>();
            _weekDayController = new WeekDayController(_mockWeekDayService.Object);
        }

        [Test]
        public async Task GetAllWeekDays_ShouldReturnOkWithWeekDays()
        {
            // Arrange
            var weekDays = new List<WeekDayModel>
            {
                new WeekDayModel { ID = 1, Name = "Monday" },
                new WeekDayModel { ID = 2, Name = "Tuesday" }
            };
            _mockWeekDayService.Setup(service => service.GetAllWeekDays()).ReturnsAsync(weekDays);

            // Act
            var result = await _weekDayController.GetAllWeekDays() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(weekDays));
        }

        [Test]
        public async Task GetWeekDay_ShouldReturnOkWithWeekDay()
        {
            // Arrange
            var weekDay = new WeekDayModel { ID = 1, Name = "Monday" };
            _mockWeekDayService.Setup(service => service.GetWeekDay(1)).ReturnsAsync(weekDay);

            // Act
            var result = await _weekDayController.GetWeekDay(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(weekDay));
        }

        [Test]
        public async Task GetWeekDay_ShouldReturnNotFoundIfWeekDayNotExists()
        {
            // Arrange
            _mockWeekDayService.Setup(service => service.GetWeekDay(1)).ReturnsAsync((WeekDayModel)null);

            // Act
            var result = await _weekDayController.GetWeekDay(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task AddWeekDay_ShouldReturnCreatedAtActionWithWeekDay()
        {
            // Arrange
            var weekDayDto = new WeekDayDto { ID = 1, Name = "Monday" };
            var weekDayModel = new WeekDayModel { ID = 1, Name = "Monday" };
            _mockWeekDayService.Setup(service => service.AddWeekDay(It.IsAny<WeekDayModel>())).ReturnsAsync(weekDayModel);

            // Act
            var result = await _weekDayController.AddWeekDay(weekDayDto) as CreatedAtActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.That(result.Value, Is.EqualTo(weekDayModel));
        }

        [Test]
        public async Task UpdateWeekDay_ShouldReturnOkWithUpdatedWeekDay()
        {
            // Arrange
            var weekDayDto = new WeekDayDto { ID = 1, Name = "Updated Monday" };
            var weekDayModel = new WeekDayModel { ID = 1, Name = "Updated Monday" };
            _mockWeekDayService.Setup(service => service.UpdateWeekDay(1, It.IsAny<WeekDayModel>())).ReturnsAsync(weekDayModel);

            // Act
            var result = await _weekDayController.UpdateWeekDay(1, weekDayDto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(weekDayModel));
        }

        [Test]
        public async Task UpdateWeekDay_ShouldReturnNotFoundIfWeekDayNotExists()
        {
            // Arrange
            var weekDayDto = new WeekDayDto { ID = 1, Name = "Updated Monday" };
            _mockWeekDayService.Setup(service => service.UpdateWeekDay(1, It.IsAny<WeekDayModel>())).ReturnsAsync((WeekDayModel)null);

            // Act
            var result = await _weekDayController.UpdateWeekDay(1, weekDayDto) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteWeekDay_ShouldReturnNoContentIfDeleted()
        {
            // Arrange
            _mockWeekDayService.Setup(service => service.DeleteWeekDay(1)).ReturnsAsync(true);

            // Act
            var result = await _weekDayController.DeleteWeekDay(1) as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task DeleteWeekDay_ShouldReturnNotFoundIfWeekDayNotExists()
        {
            // Arrange
            _mockWeekDayService.Setup(service => service.DeleteWeekDay(1)).ReturnsAsync(false);

            // Act
            var result = await _weekDayController.DeleteWeekDay(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }
    }
}
