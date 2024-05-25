using NUnit.Framework;
using Moq;
using users_microservice.models;
using users_microservice.services;
using users_microservice.repositories;

namespace test.service
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Test]
        public async Task CreateUser_WhenValidUser_ReturnsUserWithId()
        {
            // GIVEN
            var user = new UserModel
            {
                Name = "Alice",
                Email = "alice@example.com",
                Password = "password"
            };
            _mockUserRepository.Setup(repository => repository.CreateUser(user))
                               .ReturnsAsync(new UserModel { Id = 3, Name = user.Name, Email = user.Email, Password = user.Password });

            // WHEN
            var result = await _userService.CreateUser(user);

            // THEN
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(3)); // Since the initial ID was 3
                Assert.That(result.Name, Is.EqualTo(user.Name));
                Assert.That(result.Email, Is.EqualTo(user.Email));
                Assert.That(result.Password, Is.EqualTo(user.Password));
            });
        }

        [Test]
        public async Task GetUsers_WhenUsersExist_ReturnsUsersList()
        {
            // GIVEN
            var expectedUsers = new List<UserModel>
            {
                new() { Id = 1, Name = "John Doe", Email = "john@example.com", Password = "password1" },
                new() { Id = 2, Name = "Jane Doe", Email = "jane@example.com", Password = "password2" }
            };
            _mockUserRepository.Setup(repository => repository.GetUsers())
                               .ReturnsAsync(expectedUsers);

            // WHEN
            var result = await _userService.GetUsers();

            // THEN
            Assert.That(result, Has.Count.EqualTo(expectedUsers.Count));
            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(result[i].Id, Is.EqualTo(expectedUsers[i].Id));
                    Assert.That(result[i].Name, Is.EqualTo(expectedUsers[i].Name));
                    Assert.That(result[i].Email, Is.EqualTo(expectedUsers[i].Email));
                    Assert.That(result[i].Password, Is.EqualTo(expectedUsers[i].Password));
                });
            }
        }
    }
}
