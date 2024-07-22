using users_microservice.Domain.Entities;
using users_microservice.Domain.ValueObjects;

namespace users_microservice.Domain.Factory
{
    public class StudentCoursesFactory
    {
        public StudentCoursesFactory() { }

        public StudentModel CreateStudentCourses(StudentModel studentInfo, List<CourseInfo> courseInfos)
        {
            if (studentInfo == null) 
                throw new ArgumentNullException(nameof(studentInfo));
            if (courseInfos == null) 
                throw new ArgumentNullException(nameof(courseInfos));

            // Convert CourseInfo to CourseModel
            var courses = new List<CourseModel>();
            foreach (var courseInfo in courseInfos)
            {
                // Assuming CourseModel has a constructor that takes CourseInfo
                var courseModel = new CourseModel
                {
                    CourseData = courseInfo
                };
                courses.Add(courseModel);
            }

            // Assign the list of CourseModel to StudentCourses
            studentInfo.StudentCourses = courses;

            return studentInfo;
        }
    }
}
