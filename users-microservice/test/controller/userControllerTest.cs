using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using users_microservice.models;
using users_microservice.services;
using users_microservice.controllers; 

namespace test.controller;
public class UserControllerTests
{
    private Mock<IUserService> _mockUserService;
    private UserController _userController;

    [SetUp]
    public void Setup()
    {
        _mockUserService = new Mock<IUserService>();
        _userController = new UserController(_mockUserService.Object);
    }

    [Test]
    public void CreateUser_ShouldReturnCreatedUser()
    {
        var user = new UserModel { Name = "Test User", Email = "test@example.com", Password = "password" };
        _mockUserService.Setup(x => x.CreateUser(It.IsAny<UserModel>())).Returns(user);
        var result = _userController.CreateUser(user) as OkObjectResult;
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(user));
        });
    }

    [Test]
    public void GetUsers_ShouldReturnAllUsers()
    {
        var users = new List<UserModel> { new UserModel { Id = 1, Name = "User1", Email = "email1@example.com", Password = "pass1" } };
        _mockUserService.Setup(x => x.GetUsers()).Returns(users);
        var result = _userController.GetUsers() as OkObjectResult;
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(users));
        });
    }
}