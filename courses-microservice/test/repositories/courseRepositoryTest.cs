using course_microservice.context;
using course_microservice.models;
using course_microservice.repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace course_microservice.test.repositories
{
    [TestFixture]
    public class CourseRepositoryTest
    {
        private Mock<MyDbContext> _mockContext;
        private CourseRepository _repository;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<MyDbContext>();
            _repository = new CourseRepository(_mockContext.Object);
        }
        [Test]
        public async Task GetAllCourses_ShouldReturnAllCourses()
        {
            // Arrange
            IQueryable<CourseModel> myList = new List<CourseModel>
            {
                new CourseModel { ID = 1, Name = "Course 1", Credits = 3, Hours = 6, Semester = 'A', Year = 2023 },
                new CourseModel { ID = 2, Name = "Course 2", Credits = 4, Hours = 8, Semester = 'B', Year = 2022 }
            }.AsQueryable();

            var dbSetMock = MockDbSetHelper.CreateDbSetMock(myList);

            _mockContext.Setup(c => c.Course).Returns(dbSetMock.Object);

            // Act
            List<CourseModel> result = await _repository.GetAllCourses();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Course 1", result[0].Name);
            Assert.AreEqual("Course 2", result[1].Name);
        }
        [Test]
        public async Task GetCourse_ShouldReturnCourseById()
        {
            // Arrange
            int courseId = 1;
            CourseModel course = new CourseModel { ID = courseId, Name = "Course 1" };
            _mockContext.Setup(c => c.Course.FindAsync(courseId)).ReturnsAsync(course);

            // Act
            CourseModel result = await _repository.GetCourse(courseId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(courseId, result.ID);
            Assert.AreEqual("Course 1", result.Name);
        }
        [Test]
        public async Task GetCoursePrerequisites_ShouldReturnCoursePrerequisites()
        {
            // Arrange
            int courseId = 1;
            IQueryable<CoursePrerequisiteModel> coursePrerequisites = new List<CoursePrerequisiteModel>
            {
                new CoursePrerequisiteModel { CourseID = courseId, PrerequisiteCourseID = 2, PrerequisiteCourse = new CourseModel { ID = 1, Credits=3, Hours = 8, Name ="IS I", Semester = 'A', Year = 2023} },
                new CoursePrerequisiteModel { CourseID = courseId, PrerequisiteCourseID = 3, PrerequisiteCourse = new CourseModel { ID = 2, Credits=3, Hours = 8, Name ="IS II", Semester = 'A', Year = 2023} }
            }.AsQueryable();

            var dbSetMock = MockDbSetHelper.CreateDbSetMock(coursePrerequisites);

            _mockContext.Setup(c => c.CoursePrerequisites).Returns(dbSetMock.Object);

            CourseRepository repository = new CourseRepository(_mockContext.Object);

            // Act
            List<CourseModel> result = await repository.GetCoursePrerequisites(courseId);

            // Assert
            Assert.NotNull(result);  // Verificar que el resultado no sea nulo
            Assert.That(2, Is.EqualTo(result.Count));  // Verificar que se devuelvan dos cursos pre-requisitos
            // Verificar que los nombres de los cursos pre-requisitos sean correctos
            Assert.That("IS I", Is.EqualTo(result[0].Name));
            Assert.That("IS II", Is.EqualTo(result[1].Name));
        }
        [Test]
        public async Task UpdateCourse_ShouldUpdateCourse()
        {
            // Arrange
            int courseId = 1;
            CourseModel existingCourse = new CourseModel
            {
                ID = courseId,
                Name = "Course 1",
                Semester = 'A',
                Credits = 3,
                Year = 2023
            };

            CourseModel updatedCourse = new CourseModel
            {
                ID = courseId,
                Name = "Updated Course 1",
                Semester = 'B',
                Credits = 4,
                Year = 2022
            };

            // Configuración del contexto simulado (Mock<MyDbContext>)
            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.Course.FindAsync(courseId)).ReturnsAsync(existingCourse);

            CourseRepository repository = new CourseRepository(mockContext.Object);

            // Act
            CourseModel result = await repository.UpdateCourse(courseId, updatedCourse);

            // Assert
            Assert.NotNull(result);  // Verificar que el resultado no sea nulo
            Assert.That(courseId, Is.EqualTo(result.ID));  // Verificar que los IDs coinciden
            Assert.That(updatedCourse.Name, Is.EqualTo(result.Name));  // Verificar que los nombres coinciden
            Assert.That(updatedCourse.Year, Is.EqualTo(result.Year));  // Verificar que el año se actualiza correctamente

            // Verify that SaveChangesAsync was called once
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1); // Ejemplo simple, ajusta según tu necesidad
        }
        [Test]
        public async Task DeleteCourse_ShouldDeleteCourse()
        {
            // Arrange
            int courseId = 1;
            CourseModel courseToDelete = new CourseModel { ID = courseId, Name = "Course 1" };
            _mockContext.Setup(c => c.Course.FindAsync(courseId)).ReturnsAsync(courseToDelete);

            // Act
            bool result = await _repository.DeleteCourse(courseId);

            // Assert
            Assert.IsTrue(result);

            // Verify that SaveChangesAsync was called once
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1); // Ejemplo simple, ajusta según tu necesidad
        }
        [Test]
        public async Task DeleteCourse_CourseNotFound_ShouldReturnFalse()
        {
            int courseId = 1;
            _mockContext.Setup(c => c.Course.FindAsync(courseId)).ReturnsAsync((CourseModel)null);
            bool result = await _repository.DeleteCourse(courseId);
            Assert.IsFalse(result);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1); // Ejemplo simple, ajusta según tu necesidad
        }
        [Test]
        public async Task AddCourse_ShouldAddCourse()
        {
            // Arrange
            CourseModel courseToAdd = new CourseModel
            {
                ID = 1,
                Name = "New Course",
                Credits = 3,
                Hours = 6,
                Semester = 'A',
                Year = 2023
            };

            // Configuración del contexto simulado (Mock<MyDbContext>)
            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            DbSet<CourseModel> mockSet = new Mock<DbSet<CourseModel>>().Object;

            mockContext.Setup(c => c.Course).Returns(mockSet);

            CourseRepository repository = new CourseRepository(mockContext.Object);

            // Act
            CourseModel result = await repository.AddCourse(courseToAdd);

            // Assert
            Assert.NotNull(result);  // Verificar que el resultado no sea nulo
            Assert.That(courseToAdd.ID, Is.EqualTo(result.ID));  // Verificar que los IDs coinciden
            Assert.That(courseToAdd.Name, Is.EqualTo(result.Name));  // Verificar que los nombres coinciden

            // Verify that SaveChangesAsync was called once
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1); // Ejemplo simple, ajusta según tu necesidad
        }
        [Test]
        public async Task AddCoursePrerequisite_ShouldAddCoursePrerequisite()
        {
            // Arrange
            int courseId = 1;
            int prerequisiteCourseId = 2;

            CoursePrerequisiteModel addedCoursePrerequisite = null;

            // Configuración del contexto simulado (Mock<MyDbContext>)
            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();

            mockContext.Setup(c => c.CoursePrerequisites.Add(It.IsAny<CoursePrerequisiteModel>()))
                       .Callback<CoursePrerequisiteModel>(cp =>
                       {
                           addedCoursePrerequisite = cp;
                       });

            mockContext.Setup(c => c.SaveChangesAsync(default))
                       .Returns(Task.FromResult(0));

            CourseRepository repository = new CourseRepository(mockContext.Object);

            // Act
            var result = await repository.AddCoursePrerequisite(courseId, prerequisiteCourseId);

            // Assert
            Assert.That(result, Is.Not.Null);  // Verificar que el resultado no sea nulo
            Assert.That(result.CourseID, Is.EqualTo(courseId));  // Verificar que el CourseID sea el esperado
            Assert.That(result.PrerequisiteCourseID, Is.EqualTo(prerequisiteCourseId));  // Verificar que el PrerequisiteCourseID sea el esperado
            Assert.That(result, Is.SameAs(addedCoursePrerequisite));

            // Verificar que se llamó al método Add y SaveChangesAsync en el contexto
            mockContext.Verify(c => c.CoursePrerequisites.Add(It.IsAny<CoursePrerequisiteModel>()), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}
