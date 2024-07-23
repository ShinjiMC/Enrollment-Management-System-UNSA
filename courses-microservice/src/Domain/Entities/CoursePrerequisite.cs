namespace Domain.Courses
{
    public class CoursePrerequisite
    {
        public Guid CourseId { get; private set; }
        public Guid PrerequisiteCourseId { get; private set; }
        public Course? Course { get; private set; } // Marcar como anulable
        public Course? PrerequisiteCourse { get; private set; } // Marcar como anulable

        private CoursePrerequisite() { }

        public CoursePrerequisite(Guid courseId, Guid prerequisiteCourseId)
        {
            CourseId = courseId;
            PrerequisiteCourseId = prerequisiteCourseId;
        }

        // Método para asignar los cursos, si es necesario
        public void SetCourses(Course course, Course prerequisiteCourse)
        {
            Course = course;
            PrerequisiteCourse = prerequisiteCourse;
        }
    }
}
