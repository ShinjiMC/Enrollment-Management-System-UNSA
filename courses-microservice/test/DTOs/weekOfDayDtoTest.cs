using course_microservice.DTOs;

namespace course_microservice.test.DTOs
{
    [TestFixture]
    public class WeekDayDtoTest
    {
        [Test]
        public void WeekDayDto_SetAndGetProperties_ShouldReturnCorrectValues()
        {
            // Arrange
            var weekDayModel = new WeekDayDto();
            var id = 1;
            var name = "Monday";

            // Act
            weekDayModel.ID = id;
            weekDayModel.Name = name;

            // Assert
            Assert.That(weekDayModel.ID, Is.EqualTo(id));
            Assert.That(weekDayModel.Name, Is.EqualTo(name));
        }

        [Test]
        public void WeekDayDto_DefaultValues_ShouldReturnDefaultValues()
        {
            // Arrange & Act
            var weekDayModel = new WeekDayDto();

            // Assert
            Assert.That(weekDayModel.ID, Is.EqualTo(0));
            Assert.That(weekDayModel.Name, Is.Null);
        }
    }
}
