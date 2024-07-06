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
    public class SchoolRepositoryTest
    {
        [Test]
        public async Task GetAllSchools_ShouldReturnAllSchools()
        {
            // Arrange
            IQueryable<SchoolModel> schools = new List<SchoolModel>
            {
                new SchoolModel { ID = 1, Name = "School A", Faculty = "Engineering", Area = "Computer Science", FoundationDate = new DateTime(1990, 1, 1) },
                new SchoolModel { ID = 2, Name = "School B", Faculty = "Science", Area = "Biology", FoundationDate = new DateTime(1980, 5, 10) },
                new SchoolModel { ID = 3, Name = "School C", Faculty = "Business", Area = "Finance", FoundationDate = new DateTime(2000, 9, 15) }
            }.AsQueryable();

            var _mockContext = new Mock<MyDbContext>();
            var dbSetMock = MockDbSetHelper.CreateDbSetMock(schools);

            _mockContext.Setup(c => c.School).Returns(dbSetMock.Object);

            SchoolRepository repository = new SchoolRepository(_mockContext.Object);

            // Act
            List<SchoolModel> result = await repository.GetAllSchools();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("School A", result[0].Name);
            Assert.AreEqual("School B", result[1].Name);
            Assert.AreEqual("School C", result[2].Name);
        }

        [Test]
        public async Task GetSchool_ShouldReturnCorrectSchoolById()
        {
            // Arrange
            int schoolId = 1;
            SchoolModel expectedSchool = new SchoolModel { ID = schoolId, Name = "School A", Faculty = "Engineering", Area = "Computer Science", FoundationDate = new DateTime(1990, 1, 1) };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.School.FindAsync(schoolId)).ReturnsAsync(expectedSchool);

            SchoolRepository repository = new SchoolRepository(mockContext.Object);

            // Act
            SchoolModel result = await repository.GetSchool(schoolId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(schoolId, result.ID);
            Assert.AreEqual("School A", result.Name);
        }

        [Test]
        public async Task AddSchool_ShouldAddSchool()
        {
            // Arrange
            SchoolModel schoolToAdd = new SchoolModel { ID = 1, Name = "School A", Faculty = "Engineering", Area = "Computer Science", FoundationDate = new DateTime(1990, 1, 1) };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.School.Add(schoolToAdd));

            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            SchoolRepository repository = new SchoolRepository(mockContext.Object);

            // Act
            SchoolModel result = await repository.AddSchool(schoolToAdd);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.ID);
            Assert.AreEqual("School A", result.Name);
        }

        [Test]
        public async Task UpdateSchool_ShouldUpdateSchool()
        {
            // Arrange
            int schoolId = 1;
            SchoolModel existingSchool = new SchoolModel { ID = schoolId, Name = "School A", Faculty = "Engineering", Area = "Computer Science", FoundationDate = new DateTime(1990, 1, 1) };
            SchoolModel updatedSchool = new SchoolModel { ID = schoolId, Name = "Updated School A", Faculty = "Engineering", Area = "Computer Science", FoundationDate = new DateTime(1990, 1, 1) };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.School.FindAsync(schoolId)).ReturnsAsync(existingSchool);

            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            SchoolRepository repository = new SchoolRepository(mockContext.Object);

            // Act
            SchoolModel result = await repository.UpdateSchool(schoolId, updatedSchool);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(schoolId, result.ID);
            Assert.AreEqual("Updated School A", result.Name);
        }

        [Test]
        public async Task DeleteSchool_ShouldDeleteSchool()
        {
            // Arrange
            int schoolId = 1;
            SchoolModel existingSchool = new SchoolModel { ID = schoolId, Name = "School A", Faculty = "Engineering", Area = "Computer Science", FoundationDate = new DateTime(1990, 1, 1) };

            Mock<MyDbContext> mockContext = new Mock<MyDbContext>();
            mockContext.Setup(c => c.School.FindAsync(schoolId)).ReturnsAsync(existingSchool);
            mockContext.Setup(c => c.School.Remove(existingSchool));
            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));

            SchoolRepository repository = new SchoolRepository(mockContext.Object);

            // Act
            bool result = await repository.DeleteSchool(schoolId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
