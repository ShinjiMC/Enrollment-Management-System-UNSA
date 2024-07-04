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

        public DbSet<WeekDayModel> WeekDay { get; set; }
        public DbSet<CourseModel> Course { get; set; }
        public DbSet<SchoolModel> School { get; set; }
        public DbSet<ScheduleModel> Schedule { get; set; }

        // Mover la inicialización de la base de datos a un método separado
        public void InitializeDatabase()
        {
            // Añadir días de la semana en inglés
            AddWeekDays();

            // Añadir escuelas de ejemplo
            AddSchools();

            // Añadir cursos de ejemplo
            AddCourses();

            SaveChanges();

            // Añadir horarios de ejemplo
            AddSchedules();

            // Guardar cambios en la base de datos nuevamente
            SaveChanges();
        }


        private void AddCourses()
        {
            var course1 = new CourseModel
            {
                ID = 1,
                Name = "Curso 1",
                Semester = 'A',
                Credits = 3,
                Year = 3
            };

            var course2 = new CourseModel
            {
                ID = 2,
                Name = "Curso 2",
                Semester = 'B',
                Credits = 4,
                Year = 4
            };

            if (Course.Find(1) == null) Course.Add(course1);
            if (Course.Find(2) == null) Course.Add(course2);
        }

        private void AddWeekDays()
        {
            var weekDays = new[]
            {
                new WeekDayModel { ID = 1, Name = "Monday" },
                new WeekDayModel { ID = 2, Name = "Tuesday" },
                new WeekDayModel { ID = 3, Name = "Wednesday" },
                new WeekDayModel { ID = 4, Name = "Thursday" },
                new WeekDayModel { ID = 5, Name = "Friday" },
                new WeekDayModel { ID = 6, Name = "Saturday" },
                new WeekDayModel { ID = 7, Name = "Sunday" }
            };

            foreach (var weekDay in weekDays)
            {
                if (WeekDay.Find(weekDay.ID) == null) WeekDay.Add(weekDay);
            }
        }

        private void AddSchools()
        {
            var schools = new[]
            {
                new SchoolModel
                {
                    ID = 1,
                    Name = "Software Engineering",
                    Faculty = "Engineering",
                    Area = "Software",
                    FoundationDate = new DateTime(2000, 1, 1)
                },
                new SchoolModel
                {
                    ID = 2,
                    Name = "Computer Science",
                    Faculty = "Engineering",
                    Area = "Information Technology",
                    FoundationDate = new DateTime(1990, 1, 1)
                },
                new SchoolModel
                {
                    ID = 3,
                    Name = "Systems Engineering",
                    Faculty = "Engineering",
                    Area = "Systems",
                    FoundationDate = new DateTime(1985, 1, 1)
                },
                new SchoolModel
                {
                    ID = 4,
                    Name = "Art",
                    Faculty = "Humanity",
                    Area = "Humanity",
                    FoundationDate = new DateTime(2004, 1, 1)
                }
            };

            foreach (var school in schools)
            {
                if (School.Find(school.ID) == null) School.Add(school);
            }
        }

        private void AddSchedules()
        {
            var schedule1 = new ScheduleModel
            {
                ID = 1,
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
                ID = 2,
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
            if (Schedule.Find(1) == null) Schedule.Add(schedule1);
            if (Schedule.Find(2) == null) Schedule.Add(schedule2);
        }
    }
}