using NUnit.Framework;
using Moq;
using users_microservice.models;
using users_microservice.services;
using System.Linq;

namespace test.service;
public class UserServiceTests
{
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userService = new UserService();
    }

    [Test]
    public void CreateUser_ShouldAddUserToList()
    {
        var user = new UserModel { Name = "New User", Email = "newuser@example.com", Password = "test1234" };
        var createdUser = _userService.CreateUser(user);
        Assert.AreEqual(3, createdUser.Id);
        Assert.AreEqual("New User", createdUser.Name);
        Assert.IsTrue(_userService.GetUsers().Exists(u => u.Id == createdUser.Id));
    }

    [Test]
    public void GetUsers_ShouldReturnAllUsers()
    {
        var users = _userService.GetUsers();
        Assert.AreEqual(3, users.Count);
    }
}

