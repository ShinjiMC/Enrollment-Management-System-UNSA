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
    public class CourseServiceTests
    {
        private Mock<ICourseRepository> _mockCourseRepository;
        private ICourseService _courseService;

        [SetUp]
        public void SetUp()
        {
            _mockCourseRepository = new Mock<ICourseRepository>();
            _courseService = new CourseService(_mockCourseRepository.Object);
        }

        [Test]
        public async Task GetAllCourses_ShouldReturnAllCourses()
        {
            // Arrange
            var courses = new List<CourseModel>
            {
                new CourseModel { ID = 1, Name = "Course 1" },
                new CourseModel { ID = 2, Name = "Course 2" }
            };
            _mockCourseRepository.Setup(repo => repo.GetAllCourses()).ReturnsAsync(courses);

            // Act
            var result = await _courseService.GetAllCourses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(courses));
        }

        [Test]
        public async Task GetCourse_ShouldReturnCourseById()
        {
            // Arrange
            var course = new CourseModel { ID = 1, Name = "Course 1" };
            _mockCourseRepository.Setup(repo => repo.GetCourse(1)).ReturnsAsync(course);

            // Act
            var result = await _courseService.GetCourse(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(course));
        }

        [Test]
        public async Task AddCourse_ShouldAddCourse()
        {
            // Arrange
            var course = new CourseModel { ID = 1, Name = "Course 1" };
            _mockCourseRepository.Setup(repo => repo.AddCourse(course)).ReturnsAsync(course);

            // Act
            var result = await _courseService.AddCourse(course);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(course));
        }

        [Test]
        public async Task UpdateCourse_ShouldUpdateCourse()
        {
            // Arrange
            var updatedCourse = new CourseModel { ID = 1, Name = "Updated Course 1" };
            _mockCourseRepository.Setup(repo => repo.UpdateCourse(1, updatedCourse)).ReturnsAsync(updatedCourse);

            // Act
            var result = await _courseService.UpdateCourse(1, updatedCourse);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(updatedCourse));
        }

        [Test]
        public async Task DeleteCourse_ShouldReturnTrueIfCourseIsDeleted()
        {
            // Arrange
            _mockCourseRepository.Setup(repo => repo.DeleteCourse(1)).ReturnsAsync(true);

            // Act
            var result = await _courseService.DeleteCourse(1);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetCoursePrerequisites_ShouldReturnCoursePrerequisites()
        {
            // Arrange
            var prerequisites = new List<CourseModel>
            {
                new CourseModel { ID = 2, Name = "Prerequisite 1" },
                new CourseModel { ID = 3, Name = "Prerequisite 2" }
            };
            _mockCourseRepository.Setup(repo => repo.GetCoursePrerequisites(1)).ReturnsAsync(prerequisites);

            // Act
            var result = await _courseService.GetCoursePrerequisites(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(prerequisites));
        }

        [Test]
        public async Task AddCoursePrerequisite_ShouldAddCoursePrerequisite()
        {
            // Arrange
            var coursePrerequisite = new CoursePrerequisiteModel { CourseID = 1, PrerequisiteCourseID = 2 };
            _mockCourseRepository.Setup(repo => repo.AddCoursePrerequisite(1, 2)).ReturnsAsync(coursePrerequisite);

            // Act
            var result = await _courseService.AddCoursePrerequisite(1, 2);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(coursePrerequisite));
        }
    }
}
