using course_microservice.models;

namespace course_microservice.test.models
{
    [TestFixture]
    public class CourseModelTests
    {
        [Test]
        public void CourseModel_CanBeInstantiated()
        {
            // Arrange & Act
            var courseDto = new CourseModel();

            // Assert
            Assert.That(courseDto, Is.Not.Null);
        }

        [Test]
        public void CourseModel_CanSetAndGetProperties()
        {
            // Arrange
            var courseDto = new CourseModel();
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
        public void CourseModel_DefaultValuesAreSet()
        {
            // Arrange & Act
            var courseDto = new CourseModel();

            // Assert
            Assert.That(courseDto.ID, Is.EqualTo(0));
            Assert.That(courseDto.Name, Is.Empty);
            Assert.That(courseDto.Semester, Is.EqualTo(default(char)));
            Assert.That(courseDto.Credits, Is.EqualTo(0));
            Assert.That(courseDto.Year, Is.EqualTo(0));
            Assert.That(courseDto.Hours, Is.EqualTo(0));
        }
    }
}
