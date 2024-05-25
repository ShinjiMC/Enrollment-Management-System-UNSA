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
    public async Task CreateUser_WhenValidUser_ReturnsOk()
    {
        // GIVEN
        var userModel = new UserModel
        {
            Name = "Alice",
            Email = "alice@example.com",
            Password = "password3"
        };
        _mockUserService.Setup(service => service.CreateUser(userModel))
                        .ReturnsAsync(userModel);

        // WHEN
        var result = await _userController.CreateUser(userModel) as OkObjectResult;

        // THEN
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(userModel));
        });
    }

    [Test]
    public async Task GetUsers_ShouldReturnAllUsers()
    {
        // GIVEN
        var usersList = new List<UserModel>
            {
                new() { Id = 1, Name = "John Doe", Email = "john@example.com", Password = "password1" },
                new() { Id = 2, Name = "Jane Doe", Email = "jane@example.com", Password = "password2" }
            };

        _mockUserService.Setup(service => service.GetUsers())
                            .ReturnsAsync(usersList);

        // WHEN
        var result = await _userController.GetUsers() as OkObjectResult;

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(usersList));
        });
    }
}