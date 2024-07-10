using Moq;
using Microsoft.AspNetCore.Mvc;
using course_microservice.controllers;
using course_microservice.models;
using course_microservice.services;
using course_microservice.DTOs;

namespace course_microservice.test.controllers
{
    [TestFixture]
    public class SchoolControllerTests
    {
        private Mock<ISchoolService> _mockSchoolService;
        private SchoolController _schoolController;

        [SetUp]
        public void SetUp()
        {
            _mockSchoolService = new Mock<ISchoolService>();
            _schoolController = new SchoolController(_mockSchoolService.Object);
        }

        [Test]
        public async Task GetAllSchools_ShouldReturnOkWithSchools()
        {
            // Arrange
            var schools = new List<SchoolModel>
            {
                new SchoolModel { ID = 1, Name = "School 1" },
                new SchoolModel { ID = 2, Name = "School 2" }
            };
            _mockSchoolService.Setup(service => service.GetAllSchools()).ReturnsAsync(schools);

            // Act
            var result = await _schoolController.GetAllSchools() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(schools));
        }

        [Test]
        public async Task GetSchool_ShouldReturnOkWithSchool()
        {
            // Arrange
            var school = new SchoolModel { ID = 1, Name = "School 1" };
            _mockSchoolService.Setup(service => service.GetSchool(1)).ReturnsAsync(school);

            // Act
            var result = await _schoolController.GetSchool(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(school));
        }

        [Test]
        public async Task GetSchool_ShouldReturnNotFoundIfSchoolNotExists()
        {
            // Arrange
            _mockSchoolService.Setup(service => service.GetSchool(1)).ReturnsAsync((SchoolModel)null);

            // Act
            var result = await _schoolController.GetSchool(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task AddSchool_ShouldReturnCreatedAtActionWithSchool()
        {
            // Arrange
            var schoolDto = new SchoolDto { ID = 1, Name = "School 1" };
            var schoolModel = new SchoolModel { ID = 1, Name = "School 1" };
            _mockSchoolService.Setup(service => service.AddSchool(It.IsAny<SchoolModel>())).ReturnsAsync(schoolModel);

            // Act
            var result = await _schoolController.AddSchool(schoolDto) as CreatedAtActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.That(result.Value, Is.EqualTo(schoolModel));
        }

        [Test]
        public async Task UpdateSchool_ShouldReturnOkWithUpdatedSchool()
        {
            // Arrange
            var schoolDto = new SchoolDto { ID = 1, Name = "Updated School" };
            var schoolModel = new SchoolModel { ID = 1, Name = "Updated School" };
            _mockSchoolService.Setup(service => service.UpdateSchool(1, It.IsAny<SchoolModel>())).ReturnsAsync(schoolModel);

            // Act
            var result = await _schoolController.UpdateSchool(1, schoolDto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(schoolModel));
        }

        [Test]
        public async Task UpdateSchool_ShouldReturnNotFoundIfSchoolNotExists()
        {
            // Arrange
            var schoolDto = new SchoolDto { ID = 1, Name = "Updated School" };
            _mockSchoolService.Setup(service => service.UpdateSchool(1, It.IsAny<SchoolModel>())).ReturnsAsync((SchoolModel)null);

            // Act
            var result = await _schoolController.UpdateSchool(1, schoolDto) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteSchool_ShouldReturnNoContentIfDeleted()
        {
            // Arrange
            _mockSchoolService.Setup(service => service.DeleteSchool(1)).ReturnsAsync(true);

            // Act
            var result = await _schoolController.DeleteSchool(1) as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task DeleteSchool_ShouldReturnNotFoundIfSchoolNotExists()
        {
            // Arrange
            _mockSchoolService.Setup(service => service.DeleteSchool(1)).ReturnsAsync(false);

            // Act
            var result = await _schoolController.DeleteSchool(1) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }
    }
}
