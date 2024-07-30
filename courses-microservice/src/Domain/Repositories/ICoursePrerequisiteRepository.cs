namespace Domain.Courses
{
    public interface ICoursePrerequisiteRepository
    {
        Task<List<CoursePrerequisite>> GetPrerequisitesByCourseIdAsync(Guid courseId);
        void Add(CoursePrerequisite course);
        void Update(CoursePrerequisite course);
        Task<bool> DeleteAsync(Guid courseId, Guid prerequisiteCourseId);
        Task<CoursePrerequisite?> GetByIdAsync(Guid courseId, Guid coursePrerequisiteId);
    }
}