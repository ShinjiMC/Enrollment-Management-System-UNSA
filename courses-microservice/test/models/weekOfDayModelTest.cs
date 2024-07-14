using course_microservice.models;

namespace course_microservice.test.models
{
    [TestFixture]
    public class WeekDayModelTest
    {
        [Test]
        public void WeekDayModel_SetAndGetProperties_ShouldReturnCorrectValues()
        {
            // Arrange
            var weekDayModel = new WeekDayModel();
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
        public void WeekDayModel_DefaultValues_ShouldReturnDefaultValues()
        {
            // Arrange & Act
            var weekDayModel = new WeekDayModel();

            // Assert
            Assert.That(weekDayModel.ID, Is.EqualTo(0));
            Assert.That(weekDayModel.Name, Is.Null);
        }
    }
}
