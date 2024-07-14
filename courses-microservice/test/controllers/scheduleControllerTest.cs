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
    public class ScheduleControllerTests
    {
        private Mock<IScheduleService> _mockScheduleService;
        private ScheduleController _scheduleController;

        [SetUp]
        public void SetUp()
        {
            _mockScheduleService = new Mock<IScheduleService>();
            _scheduleController = new ScheduleController(_mockScheduleService.Object);
        }

        [Test]
        public async Task GetAllSchedules_ShouldReturnOkWithSchedules()
        {
            // Arrange
            var schedules = new List<ScheduleModel>
            {
                new ScheduleModel { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 },
                new ScheduleModel { ID = 2, CourseID = 102, WeekDayID = 2, SchoolID = 1, Group = "B", Year = 2023 }
            };
            _mockScheduleService.Setup(service => service.GetAllSchedules()).ReturnsAsync(schedules);

            // Act
            var result = await _scheduleController.GetAllSchedules() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(schedules));
        }

        [Test]
        public async Task GetSchedule_ShouldReturnOkWithSchedule()
        {
            // Arrange
            var schedule = new ScheduleModel { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 };
            _mockScheduleService.Setup(service => service.GetSchedule(1)).ReturnsAsync(schedule);

            // Act
            var result = await _scheduleController.GetSchedule(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(schedule));
        }

        [Test]
        public async Task GetSchedule_ShouldReturnNotFoundIfScheduleNotExists()
        {
            // Arrange
            _mockScheduleService.Setup(service => service.GetSchedule(1)).ReturnsAsync((ScheduleModel)null);

            // Act
            var result = await _scheduleController.GetSchedule(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task AddSchedule_ShouldReturnCreatedAtActionWithSchedule()
        {
            // Arrange
            var scheduleDto = new ScheduleDto { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 };
            var scheduleModel = new ScheduleModel { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 };
            _mockScheduleService.Setup(service => service.AddSchedule(It.IsAny<ScheduleModel>())).ReturnsAsync(scheduleModel);

            // Act
            var result = await _scheduleController.AddSchedule(scheduleDto) as CreatedAtActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.That(result.Value, Is.EqualTo(scheduleModel));
        }

        [Test]
        public async Task AddSchedule_ShouldReturnBadRequestIfDuplicate()
        {
            // Arrange
            var scheduleDto = new ScheduleDto { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 };
            _mockScheduleService.Setup(service => service.AddSchedule(It.IsAny<ScheduleModel>())).ReturnsAsync((ScheduleModel)null);

            // Act
            var result = await _scheduleController.AddSchedule(scheduleDto) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("A schedule with the same combination of CourseID, DayOfWeekID, SchoolID, Group, and Year already exists."));
        }

        [Test]
        public async Task UpdateSchedule_ShouldReturnOkWithUpdatedSchedule()
        {
            // Arrange
            var scheduleDto = new ScheduleDto { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 };
            var scheduleModel = new ScheduleModel { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 };
            _mockScheduleService.Setup(service => service.UpdateSchedule(1, It.IsAny<ScheduleModel>())).ReturnsAsync(scheduleModel);

            // Act
            var result = await _scheduleController.UpdateSchedule(1, scheduleDto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(scheduleModel));
        }

        [Test]
        public async Task UpdateSchedule_ShouldReturnNotFoundIfScheduleNotExists()
        {
            // Arrange
            var scheduleDto = new ScheduleDto { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 };
            _mockScheduleService.Setup(service => service.UpdateSchedule(1, It.IsAny<ScheduleModel>())).ReturnsAsync((ScheduleModel)null);

            // Act
            var result = await _scheduleController.UpdateSchedule(1, scheduleDto) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteSchedule_ShouldReturnNoContentIfDeleted()
        {
            // Arrange
            _mockScheduleService.Setup(service => service.DeleteSchedule(1)).ReturnsAsync(true);

            // Act
            var result = await _scheduleController.DeleteSchedule(1) as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task DeleteSchedule_ShouldReturnNotFoundIfScheduleNotExists()
        {
            // Arrange
            _mockScheduleService.Setup(service => service.DeleteSchedule(1)).ReturnsAsync(false);

            // Act
            var result = await _scheduleController.DeleteSchedule(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetSchedulesByYearSemesterSchool_ShouldReturnOkWithSchedules()
        {
            // Arrange
            var schedules = new List<ScheduleModel>
            {
                new ScheduleModel { ID = 1, CourseID = 101, WeekDayID = 1, SchoolID = 1, Group = "A", Year = 2023 },
                new ScheduleModel { ID = 2, CourseID = 102, WeekDayID = 2, SchoolID = 1, Group = "B", Year = 2023 }
            };
            _mockScheduleService.Setup(service => service.GetSchedulesByYearSemesterSchool(2023, 'A', 1)).ReturnsAsync(schedules);

            // Act
            var result = await _scheduleController.GetSchedulesByYearSemesterSchool(2023, 'A', 1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(schedules));
        }
    }
}
