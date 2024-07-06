using Moq;
using course_microservice.models;
using course_microservice.repositories;
using course_microservice.services;

namespace course_microservice.test.services
{
    [TestFixture]
    public class SchoolServiceTests
    {
        private Mock<ISchoolRepository> _mockSchoolRepository;
        private ISchoolService _schoolService;

        [SetUp]
        public void SetUp()
        {
            _mockSchoolRepository = new Mock<ISchoolRepository>();
            _schoolService = new SchoolService(_mockSchoolRepository.Object);
        }

        [Test]
        public async Task GetAllSchools_ShouldReturnAllSchools()
        {
            // Arrange
            var schools = new List<SchoolModel>
            {
                new SchoolModel { ID = 1, Name = "School 1" },
                new SchoolModel { ID = 2, Name = "School 2" }
            };
            _mockSchoolRepository.Setup(repo => repo.GetAllSchools()).ReturnsAsync(schools);

            // Act
            var result = await _schoolService.GetAllSchools();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(schools));
        }

        [Test]
        public async Task GetSchool_ShouldReturnSchoolById()
        {
            // Arrange
            var school = new SchoolModel { ID = 1, Name = "School 1" };
            _mockSchoolRepository.Setup(repo => repo.GetSchool(1)).ReturnsAsync(school);

            // Act
            var result = await _schoolService.GetSchool(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(school));
        }

        [Test]
        public async Task AddSchool_ShouldAddSchool()
        {
            // Arrange
            var school = new SchoolModel { ID = 1, Name = "School 1" };
            _mockSchoolRepository.Setup(repo => repo.AddSchool(school)).ReturnsAsync(school);

            // Act
            var result = await _schoolService.AddSchool(school);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(school));
        }

        [Test]
        public async Task UpdateSchool_ShouldUpdateSchool()
        {
            // Arrange
            var updatedSchool = new SchoolModel { ID = 1, Name = "Updated School 1" };
            _mockSchoolRepository.Setup(repo => repo.UpdateSchool(1, updatedSchool)).ReturnsAsync(updatedSchool);

            // Act
            var result = await _schoolService.UpdateSchool(1, updatedSchool);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(updatedSchool));
        }

        [Test]
        public async Task DeleteSchool_ShouldReturnTrueIfSchoolIsDeleted()
        {
            // Arrange
            _mockSchoolRepository.Setup(repo => repo.DeleteSchool(1)).ReturnsAsync(true);

            // Act
            var result = await _schoolService.DeleteSchool(1);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
