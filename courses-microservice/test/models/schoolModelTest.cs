using course_microservice.models;

namespace course_microservice.test.models
{
    [TestFixture]
    public class SchoolModelTest
    {
        [Test]
        public void SchoolModel_SetAndGetProperties_ShouldReturnCorrectValues()
        {
            // Arrange
            var schoolDto = new SchoolModel();
            var id = 1;
            var name = "Engineering School";
            var faculty = "Engineering";
            var area = "Science and Technology";
            var foundationDate = new DateTime(1960, 1, 1,1,1,1,DateTimeKind.Utc);

            // Act
            schoolDto.ID = id;
            schoolDto.Name = name;
            schoolDto.Faculty = faculty;
            schoolDto.Area = area;
            schoolDto.FoundationDate = foundationDate;

            // Assert
            Assert.That(schoolDto.ID, Is.EqualTo(id));
            Assert.That(schoolDto.Name, Is.EqualTo(name));
            Assert.That(schoolDto.Faculty, Is.EqualTo(faculty));
            Assert.That(schoolDto.Area, Is.EqualTo(area));
            Assert.That(schoolDto.FoundationDate, Is.EqualTo(foundationDate));
        }

        [Test]
        public void SchoolModel_DefaultValues_ShouldReturnDefaultValues()
        {
            // Arrange & Act
            var schoolDto = new SchoolModel();

            // Assert
            Assert.That(schoolDto.ID, Is.EqualTo(0));
            Assert.That(schoolDto.Name, Is.Null);
            Assert.That(schoolDto.Faculty, Is.Null);
            Assert.That(schoolDto.Area, Is.Null);
            Assert.That(schoolDto.FoundationDate, Is.EqualTo(default(DateTime)));
        }
    }
}
