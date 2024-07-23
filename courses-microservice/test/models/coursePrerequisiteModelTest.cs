using course_microservice.models;

namespace course_microservice.test.models
{
    [TestFixture]
    public class CoursePrerequisiteModelTest
    {
        [Test]
        public void CoursePrerequisiteModel_CanBeInstantiated()
        {
            // Arrange & Act
            var coursePrerequisiteModel = new CoursePrerequisiteModel();

            // Assert
            Assert.That(coursePrerequisiteModel, Is.Not.Null);
        }

        [Test]
        public void CoursePrerequisiteModel_CanSetAndGetProperties()
        {
            // Arrange
            var coursePrerequisite = new CoursePrerequisiteModel();
            int id = 1;
            int prerequisiteCourseID = 1;
            int courseId = 2;
            var courseM = new CourseModel {Credits=4,Hours=8,ID=2,Name="IS I",Semester='A',Year=2023};
            var courseN = new CourseModel {Credits=4,Hours=8,ID=1,Name="IS II",Semester='A',Year=2023};
            // Act
            coursePrerequisite.ID = id;
            coursePrerequisite.CourseID = courseId;
            coursePrerequisite.PrerequisiteCourseID = prerequisiteCourseID;
            coursePrerequisite.Course = courseM;
            coursePrerequisite.PrerequisiteCourse = courseN;

            // Assert
            Assert.That(coursePrerequisite.ID, Is.EqualTo(id));
            Assert.That(coursePrerequisite.CourseID, Is.EqualTo(courseId));
            Assert.That(coursePrerequisite.PrerequisiteCourseID, Is.EqualTo(prerequisiteCourseID));
            Assert.That(coursePrerequisite.Course, Is.EqualTo(courseM));
            Assert.That(coursePrerequisite.PrerequisiteCourse, Is.EqualTo(courseN));
        }

        [Test]
        public void CourseModel_DefaultValuesAreSet()
        {
            // Arrange & Act
            var coursePrerequisiteModel = new CoursePrerequisiteModel();

            // Assert
            Assert.That(coursePrerequisiteModel.ID, Is.EqualTo(0));
            Assert.That(coursePrerequisiteModel.CourseID, Is.EqualTo(0));
            Assert.That(coursePrerequisiteModel.PrerequisiteCourseID, Is.EqualTo(0));
            Assert.That(coursePrerequisiteModel.Course, Is.Null);
            Assert.That(coursePrerequisiteModel.PrerequisiteCourse, Is.Null);
        }
    }
}
