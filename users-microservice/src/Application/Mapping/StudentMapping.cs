using users_microservice.Domain.Entities;
using users_microservice.Application.Dtos;
using users_microservice.Domain.ValueObjects;

namespace users_microservice.Application.Mapping
{
    public static class StudentMapping
    {
        public static StudentDto ToDto(this StudentModel student)
        {
            return new StudentDto
            {
                FullName = student.FullName ?? string.Empty, // Maneja posibles valores null
                Email = student.Email ?? string.Empty, // Maneja posibles valores null
                Cui = student.StudentData?.Cui ?? string.Empty, // Maneja posibles valores null en StudentData
                CourseIds = (student.StudentCourses ?? new List<CourseModel>())
                            .Where(sc => sc.CourseData != null) 
                            .Select(sc => sc.CourseData!.CourseId)
                            .Where(id => id != null) 
                            .ToList(),
                AcademicPerformance = student.AcademicPerformance,
                Credit = student.Credit,
                SchoolId = student.StudentData?.SchoolId

                // Mapea otros campos según sea necesario
            };
        }
        public static ExtStudentCourseDto ToExtDto(this StudentModel student)
        {
            return new ExtStudentCourseDto
            {
                FullName = student.FullName ?? string.Empty, // Maneja posibles valores null
                AcademicPerformance = student.AcademicPerformance,
                Credit = student.Credit,
                CourseIds = (student.StudentCourses ?? new List<CourseModel>())
                            .Where(sc => sc.CourseData != null) 
                            .Select(sc => sc.CourseData!.CourseId)
                            .Where(id => id != null) 
                            .ToList()
            };
        }
        public static StudentModel ToModel(StudentDto studentDto)
        {
            var valueObjectStudenInfo = new StudentInfo(studentDto.Cui, studentDto.SchoolId);

            return new StudentModel
            {
                FullName = studentDto.FullName,
                Email = studentDto.Email,
                StudentData =  valueObjectStudenInfo,
                AcademicPerformance = studentDto.AcademicPerformance,
                Credit = studentDto.Credit,
            };
        }
        public static bool IsEmpty(this StudentDto studentDto)
        {
            return string.IsNullOrEmpty(studentDto.FullName) &&
                string.IsNullOrEmpty(studentDto.Email) &&
                (studentDto.UserData == null || string.IsNullOrEmpty(studentDto.UserData.UserName)) &&
                !studentDto.CourseIds.Any() &&
                studentDto.AcademicPerformance == null &&
                studentDto.Credit == null &&
                string.IsNullOrEmpty(studentDto.SchoolId);
        }
    }
}
