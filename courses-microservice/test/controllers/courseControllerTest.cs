using Moq;
using Microsoft.AspNetCore.Mvc;
using course_microservice.controllers;
using course_microservice.models;
using course_microservice.services;
using course_microservice.DTOs;

namespace course_microservice.test.controllers
{
    [TestFixture]
    public class CourseControllerTests
    {
        private Mock<ICourseService> _mockCourseService;
        private CourseController _courseController;

        [SetUp]
        public void SetUp()
        {
            _mockCourseService = new Mock<ICourseService>();
            _courseController = new CourseController(_mockCourseService.Object);
        }

        [Test]
        public async Task GetAllCourses_ShouldReturnOkWithCourses()
        {
            // Arrange
            var courses = new List<CourseModel>
            {
                new CourseModel { ID = 1, Name = "Course 1" },
                new CourseModel { ID = 2, Name = "Course 2" }
            };
            _mockCourseService.Setup(service => service.GetAllCourses()).ReturnsAsync(courses);

            // Act
            var result = await _courseController.GetAllCourses() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(courses));
        }

        [Test]
        public async Task GetCourse_ShouldReturnOkWithCourse()
        {
            // Arrange
            var course = new CourseModel { ID = 1, Name = "Course 1" };
            _mockCourseService.Setup(service => service.GetCourse(1)).ReturnsAsync(course);

            // Act
            var result = await _courseController.GetCourse(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(course));
        }

        [Test]
        public async Task GetCourse_ShouldReturnNotFoundIfCourseNotExists()
        {
            // Arrange
            _mockCourseService.Setup(service => service.GetCourse(1)).ReturnsAsync((CourseModel)null);

            // Act
            var result = await _courseController.GetCourse(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task AddCourse_ShouldReturnCreatedAtActionWithCourse()
        {
            // Arrange
            var courseDto = new CourseDto { ID = 1, Name = "Course 1" };
            var courseModel = new CourseModel { ID = 1, Name = "Course 1" };
            _mockCourseService.Setup(service => service.AddCourse(It.IsAny<CourseModel>())).ReturnsAsync(courseModel);

            // Act
            var result = await _courseController.AddCourse(courseDto) as CreatedAtActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.That(result.Value, Is.EqualTo(courseModel));
        }

        [Test]
        public async Task UpdateCourse_ShouldReturnOkWithUpdatedCourse()
        {
            // Arrange
            var courseDto = new CourseDto { ID = 1, Name = "Updated Course" };
            var courseModel = new CourseModel { ID = 1, Name = "Updated Course" };
            _mockCourseService.Setup(service => service.UpdateCourse(1, It.IsAny<CourseModel>())).ReturnsAsync(courseModel);

            // Act
            var result = await _courseController.UpdateCourse(1, courseDto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(courseModel));
        }

        [Test]
        public async Task UpdateCourse_ShouldReturnNotFoundIfCourseNotExists()
        {
            // Arrange
            var courseDto = new CourseDto { ID = 1, Name = "Updated Course" };
            _mockCourseService.Setup(service => service.UpdateCourse(1, It.IsAny<CourseModel>())).ReturnsAsync((CourseModel)null);

            // Act
            var result = await _courseController.UpdateCourse(1, courseDto) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteCourse_ShouldReturnNoContentIfDeleted()
        {
            // Arrange
            _mockCourseService.Setup(service => service.DeleteCourse(1)).ReturnsAsync(true);

            // Act
            var result = await _courseController.DeleteCourse(1) as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task DeleteCourse_ShouldReturnNotFoundIfCourseNotExists()
        {
            // Arrange
            _mockCourseService.Setup(service => service.DeleteCourse(1)).ReturnsAsync(false);

            // Act
            var result = await _courseController.DeleteCourse(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetCoursePrerequisites_ShouldReturnOkWithPrerequisites()
        {
            // Arrange
            var prerequisites = new List<CourseModel>
            {
                new CourseModel { ID = 1, Name = "Prerequisite 1" },
                new CourseModel { ID = 2, Name = "Prerequisite 2" }
            };
            _mockCourseService.Setup(service => service.GetCoursePrerequisites(1)).ReturnsAsync(prerequisites);

            // Act
            var result = await _courseController.GetCoursePrerequisites(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(prerequisites));
        }

        [Test]
        public async Task AddCoursePrerequisite_ShouldReturnCreatedAtActionWithPrerequisite()
        {
            // Arrange
            var prerequisite = new CoursePrerequisiteModel { CourseID = 1, PrerequisiteCourseID = 2 };
            _mockCourseService.Setup(service => service.AddCoursePrerequisite(1, 2)).ReturnsAsync(prerequisite);

            // Act
            var result = await _courseController.AddCoursePrerequisite(1, 2) as CreatedAtActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.That(result.Value, Is.EqualTo(prerequisite));
        }
    }
}
