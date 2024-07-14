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
    public class ScheduleServiceTests
    {
        private Mock<IScheduleRepository> _mockScheduleRepository;
        private IScheduleService _scheduleService;

        [SetUp]
        public void SetUp()
        {
            _mockScheduleRepository = new Mock<IScheduleRepository>();
            _scheduleService = new ScheduleService(_mockScheduleRepository.Object);
        }

        [Test]
        public async Task GetAllSchedules_ShouldReturnAllSchedules()
        {
            // Arrange
            var schedules = new List<ScheduleModel>
            {
                new ScheduleModel { ID = 1, TeacherFullName = "Teacher 1" },
                new ScheduleModel { ID = 2, TeacherFullName = "Teacher 2" }
            };
            _mockScheduleRepository.Setup(repo => repo.GetAllSchedules()).ReturnsAsync(schedules);

            // Act
            var result = await _scheduleService.GetAllSchedules();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(schedules));
        }

        [Test]
        public async Task GetSchedule_ShouldReturnScheduleById()
        {
            // Arrange
            var schedule = new ScheduleModel { ID = 1, TeacherFullName = "Teacher 1" };
            _mockScheduleRepository.Setup(repo => repo.GetSchedule(1)).ReturnsAsync(schedule);

            // Act
            var result = await _scheduleService.GetSchedule(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(schedule));
        }

        [Test]
        public async Task AddSchedule_ShouldAddSchedule()
        {
            // Arrange
            var schedule = new ScheduleModel { ID = 1, TeacherFullName = "Teacher 1" };
            _mockScheduleRepository.Setup(repo => repo.AddSchedule(schedule)).ReturnsAsync(schedule);

            // Act
            var result = await _scheduleService.AddSchedule(schedule);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(schedule));
        }

        [Test]
        public async Task UpdateSchedule_ShouldUpdateSchedule()
        {
            // Arrange
            var updatedSchedule = new ScheduleModel { ID = 1, TeacherFullName = "Updated Teacher 1" };
            _mockScheduleRepository.Setup(repo => repo.UpdateSchedule(1, updatedSchedule)).ReturnsAsync(updatedSchedule);

            // Act
            var result = await _scheduleService.UpdateSchedule(1, updatedSchedule);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(updatedSchedule));
        }

        [Test]
        public async Task DeleteSchedule_ShouldReturnTrueIfScheduleIsDeleted()
        {
            // Arrange
            _mockScheduleRepository.Setup(repo => repo.DeleteSchedule(1)).ReturnsAsync(true);

            // Act
            var result = await _scheduleService.DeleteSchedule(1);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetSchedulesByYearSemesterSchool_ShouldReturnSchedulesByYearSemesterSchool()
        {
            // Arrange
            var schedules = new List<ScheduleModel>
            {
                new ScheduleModel { ID = 1, Year = 2023, TeacherFullName = "Teacher 1" },
                new ScheduleModel { ID = 2, Year = 2023, TeacherFullName = "Teacher 2" }
            };
            _mockScheduleRepository.Setup(repo => repo.GetSchedulesByYearSemesterSchool(2023, 'A', 1)).ReturnsAsync(schedules);

            // Act
            var result = await _scheduleService.GetSchedulesByYearSemesterSchool(2023, 'A', 1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(schedules));
        }
    }
}
