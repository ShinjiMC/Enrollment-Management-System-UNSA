using course_microservice.models;

namespace course_microservice.test.models
{
    [TestFixture]
    public class ScheduleModelTest
    {
        [Test]
        public void ScheduleModel_SetAndGetProperties_ShouldReturnCorrectValues()
        {
            // Arrange
            var scheduleDto = new ScheduleModel();
            var id = 1;
            var courseId = 101;
            var weekDayId = 5;
            var schoolId = 20;
            var group = "A1";
            var year = 2024;
            var startTime = new DateTime(2024, 7, 4, 9, 0, 0, DateTimeKind.Utc);
            var endTime = new DateTime(2024, 7, 4, 11, 0, 0, DateTimeKind.Utc);
            var teacherFullName = "John Doe";
            var course = new CourseModel { ID = 101, Name = "Math", Semester = 'S', Credits = 3, Year = 2024, Hours = 60 };
            var weekDay = new WeekDayModel { ID = 5, Name = "Friday" };
            var school = new SchoolModel { ID = 20, Name = "Engineering", Faculty = "Science", Area = "Technology", FoundationDate = new DateTime(1990, 1, 1, 11, 0, 0, DateTimeKind.Utc) };

            // Act
            scheduleDto.ID = id;
            scheduleDto.CourseID = courseId;
            scheduleDto.WeekDayID = weekDayId;
            scheduleDto.SchoolID = schoolId;
            scheduleDto.Group = group;
            scheduleDto.Year = year;
            scheduleDto.StartTime = startTime;
            scheduleDto.EndTime = endTime;
            scheduleDto.TeacherFullName = teacherFullName;
            scheduleDto.Course = course;
            scheduleDto.WeekDay = weekDay;
            scheduleDto.School = school;

            // Assert
            Assert.That(scheduleDto.ID, Is.EqualTo(id));
            Assert.That(scheduleDto.CourseID, Is.EqualTo(courseId));
            Assert.That(scheduleDto.WeekDayID, Is.EqualTo(weekDayId));
            Assert.That(scheduleDto.SchoolID, Is.EqualTo(schoolId));
            Assert.That(scheduleDto.Group, Is.EqualTo(group));
            Assert.That(scheduleDto.Year, Is.EqualTo(year));
            Assert.That(scheduleDto.StartTime, Is.EqualTo(startTime));
            Assert.That(scheduleDto.EndTime, Is.EqualTo(endTime));
            Assert.That(scheduleDto.TeacherFullName, Is.EqualTo(teacherFullName));
            Assert.That(scheduleDto.Course, Is.EqualTo(course));
            Assert.That(scheduleDto.WeekDay, Is.EqualTo(weekDay));
            Assert.That(scheduleDto.School, Is.EqualTo(school));
        }

        [Test]
        public void ScheduleModel_DefaultValues_ShouldReturnDefaultValues()
        {
            // Arrange & Act
            var scheduleDto = new ScheduleModel();

            // Assert
            Assert.That(scheduleDto.ID, Is.EqualTo(0));
            Assert.That(scheduleDto.CourseID, Is.EqualTo(0));
            Assert.That(scheduleDto.WeekDayID, Is.EqualTo(0));
            Assert.That(scheduleDto.SchoolID, Is.EqualTo(0));
            Assert.That(scheduleDto.Group, Is.EqualTo(string.Empty));
            Assert.That(scheduleDto.Year, Is.EqualTo(0));
            Assert.That(scheduleDto.StartTime, Is.EqualTo(default(DateTime)));
            Assert.That(scheduleDto.EndTime, Is.EqualTo(default(DateTime)));
            Assert.That(scheduleDto.TeacherFullName, Is.EqualTo(string.Empty));
            Assert.That(scheduleDto.Course, Is.Null);
            Assert.That(scheduleDto.WeekDay, Is.Null);
            Assert.That(scheduleDto.School, Is.Null);
        }
    }
}
