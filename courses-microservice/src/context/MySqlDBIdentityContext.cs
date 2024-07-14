using Microsoft.EntityFrameworkCore;
using course_microservice.models;
using course_microservice.DTOs;
using System;

namespace course_microservice.context
{
    
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            InitializeDatabase();
        }
        public MyDbContext() {}

        public DbSet<WeekDayModel> WeekDay { get; set; }
        public DbSet<CourseModel> Course { get; set; }
        public DbSet<SchoolModel> School { get; set; }
        public DbSet<ScheduleModel> Schedule { get; set; }
        public DbSet<CoursePrerequisiteModel> CoursePrerequisites { get; set; }

        public void InitializeDatabase()
        {
            // Añadir días de la semana en inglés
            AddWeekDays();

            // Añadir escuelas de ejemplo
            AddSchools();

            // Añadir cursos de ejemplo
            AddCourses();

            SaveChanges();

            AddCoursePrerequisites();

            // Añadir horarios de ejemplo
            AddSchedules();

            // Guardar cambios en la base de datos nuevamente
            SaveChanges();
        }


        private void AddCourses()
        {
            var course1 = new CourseModel
            {
                Name = "Ingenieria de Software I",
                Semester = 'A',
                Credits = 3,
                Year = 3,
                Hours = 8
            };

            var course2 = new CourseModel
            {
                Name = "Ingenieria de Software II",
                Semester = 'B',
                Credits = 4,
                Year = 3,
                Hours = 6
            };

            var course3 = new CourseModel
            {
                Name = "Ingenieria de Software III",
                Semester = 'A',
                Credits = 4,
                Year = 4,
                Hours = 8
            };

            Course.Add(course1);
            Course.Add(course2);
            Course.Add(course3);

        }
        private void AddCoursePrerequisites(){
            var prerequisite1 = new CoursePrerequisiteModel{
                CourseID = 3,
                PrerequisiteCourseID = 2
            };
            var prerequisite2 = new CoursePrerequisiteModel{
                CourseID = 2,
                PrerequisiteCourseID = 1
            };

            CoursePrerequisites.Add(prerequisite1);
            CoursePrerequisites.Add(prerequisite2);
        }

        private void AddWeekDays()
        {
            var weekDays = new[]
            {
                new WeekDayModel { Name = "Monday" },
                new WeekDayModel { Name = "Tuesday" },
                new WeekDayModel { Name = "Wednesday" },
                new WeekDayModel { Name = "Thursday" },
                new WeekDayModel { Name = "Friday" },
                new WeekDayModel { Name = "Saturday" },
                new WeekDayModel { Name = "Sunday" }
            };

            foreach (var weekDay in weekDays)
            {
                WeekDay.Add(weekDay);
            }
        }

        private void AddSchools()
        {
            var schools = new[]
            {
                new SchoolModel
                {
                    Name = "Software Engineering",
                    Faculty = "Engineering",
                    Area = "Software",
                    FoundationDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SchoolModel
                {
                    Name = "Computer Science",
                    Faculty = "Engineering",
                    Area = "Information Technology",
                    FoundationDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SchoolModel
                {
                    Name = "Systems Engineering",
                    Faculty = "Engineering",
                    Area = "Systems",
                    FoundationDate = new DateTime(1985, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SchoolModel
                {
                    Name = "Art",
                    Faculty = "Humanity",
                    Area = "Humanity",
                    FoundationDate = new DateTime(2004, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };


            foreach (var school in schools)
            {
                School.Add(school);
            }
        }

        private void AddSchedules()
        {
            var schedule1 = new ScheduleModel
            {
                CourseID = 1,
                WeekDayID = 1,
                SchoolID = 1,
                Group = "A",
                Year = 2024,
                StartTime = new DateTime(2024, 6, 17, 9, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2024, 6, 17, 10, 30, 0, DateTimeKind.Utc),
                TeacherFullName = "Prof. John Doe",
                Capacity = 30
            };

            var schedule2 = new ScheduleModel
            {
                CourseID = 2,
                WeekDayID = 2,
                SchoolID = 2,
                Group = "B",
                Year = 2024,
                StartTime = new DateTime(2024, 6, 18, 11, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2024, 6, 18, 12, 30, 0, DateTimeKind.Utc),
                TeacherFullName = "Dr. Jane Smith",
                Capacity = 25
            };
            Schedule.Add(schedule1);
            Schedule.Add(schedule2);
        }
    }
}