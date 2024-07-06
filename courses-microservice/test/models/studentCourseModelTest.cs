using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using course_microservice.models;

namespace course_microservice.test.models
{
    [TestFixture]
    public class StudentCourseModelTests
    {
        [Test]
        public void StudentCourseModel_CanBeInstantiated()
        {
            // Arrange & Act
            var studentCourseModel = new StudentCourseModel();

            // Assert
            Assert.That(studentCourseModel, Is.Not.Null);
        }

        [Test]
        public void StudentCourseModel_CanSetAndGetProperties()
        {
            // Arrange
            var studentCourseModel = new StudentCourseModel();
            int id = 1;
            int studentId = 101;
            int courseId = 201;
            char semester = 'A';
            int year = 2023;

            // Act
            studentCourseModel.StudentCourseID = id;
            studentCourseModel.StudentID = studentId;
            studentCourseModel.CourseID = courseId;
            studentCourseModel.Semester = semester;
            studentCourseModel.Year = year;

            // Assert
            Assert.That(studentCourseModel.StudentCourseID, Is.EqualTo(id));
            Assert.That(studentCourseModel.StudentID, Is.EqualTo(studentId));
            Assert.That(studentCourseModel.CourseID, Is.EqualTo(courseId));
            Assert.That(studentCourseModel.Semester, Is.EqualTo(semester));
            Assert.That(studentCourseModel.Year, Is.EqualTo(year));
        }

        [Test]
        public void StudentCourseModel_DefaultValuesAreSet()
        {
            // Arrange & Act
            var studentCourseModel = new StudentCourseModel();

            // Assert
            Assert.That(studentCourseModel.StudentCourseID, Is.EqualTo(0));
            Assert.That(studentCourseModel.StudentID, Is.EqualTo(0));
            Assert.That(studentCourseModel.CourseID, Is.EqualTo(0));
            Assert.That(studentCourseModel.Semester, Is.EqualTo(default(char)));
            Assert.That(studentCourseModel.Year, Is.EqualTo(0));
        }
    }
}
