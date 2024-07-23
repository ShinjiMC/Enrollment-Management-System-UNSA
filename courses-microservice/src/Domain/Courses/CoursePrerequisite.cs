namespace Domain.Courses
{
    public class CoursePrerequisite
    {
        public CourseId CourseId { get; private set; }
        public CourseId PrerequisiteCourseId { get; private set; }
        public Course Course { get; private set; }
        public Course PrerequisiteCourse { get; private set; }

        private CoursePrerequisite() { }

        public CoursePrerequisite(CourseId courseId, CourseId prerequisiteCourseId)
        {
            CourseId = courseId;
            PrerequisiteCourseId = prerequisiteCourseId;
        }
    }
}
