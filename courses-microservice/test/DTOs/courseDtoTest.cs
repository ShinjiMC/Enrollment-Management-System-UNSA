using course_microservice.DTOs;

namespace course_microservice.test.DTOs
{
    [TestFixture]
    public class CourseDtoTests
    {
        [Test]
        public void CourseDto_CanBeInstantiated()
        {
            // Arrange & Act
            var courseDto = new CourseDto();

            // Assert
            Assert.That(courseDto, Is.Not.Null);
        }

        [Test]
        public void CourseDto_CanSetAndGetProperties()
        {
            // Arrange
            var courseDto = new CourseDto();
            int id = 1;
            string name = "Mathematics";
            char semester = 'A';
            int credits = 3;
            int year = 2023;
            int hours = 120;

            // Act
            courseDto.ID = id;
            courseDto.Name = name;
            courseDto.Semester = semester;
            courseDto.Credits = credits;
            courseDto.Year = year;
            courseDto.Hours = hours;

            // Assert
            Assert.That(courseDto.ID, Is.EqualTo(id));
            Assert.That(courseDto.Name, Is.EqualTo(name));
            Assert.That(courseDto.Semester, Is.EqualTo(semester));
            Assert.That(courseDto.Credits, Is.EqualTo(credits));
            Assert.That(courseDto.Year, Is.EqualTo(year));
            Assert.That(courseDto.Hours, Is.EqualTo(hours));
        }

        [Test]
        public void CourseDto_DefaultValuesAreSet()
        {
            // Arrange & Act
            var courseDto = new CourseDto();

            // Assert
            Assert.That(courseDto.ID, Is.EqualTo(0));
            Assert.That(courseDto.Name, Is.Null);
            Assert.That(courseDto.Semester, Is.EqualTo(default(char)));
            Assert.That(courseDto.Credits, Is.EqualTo(0));
            Assert.That(courseDto.Year, Is.EqualTo(0));
            Assert.That(courseDto.Hours, Is.EqualTo(0));
        }
    }
}
