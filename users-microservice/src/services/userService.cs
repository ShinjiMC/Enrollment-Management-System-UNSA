using System.Collections.Generic;
using users_microservice.models;

namespace users_microservice.services;

public interface IUserService
{
    UserModel CreateUser(UserModel user);
    List<UserModel> GetUsers();
}

public class UserService : IUserService
{
    private int nextId = 3;
    private static List<UserModel> Users = new List<UserModel>
    {
        new UserModel
        {
            Id = 1,
            Name = "John Doe",
            Email = "Example@gmail.com",
            Password = "password"
        },
        new UserModel
        {
            Id = 2,
            Name = "Jane Doe",
            Email = "xample2@gmail.com",
            Password = "password2"
        }
    };
    public UserModel CreateUser(UserModel user)
    {
        user.Id = nextId++;
        Users.Add(user);
        return user;
    }
    public List<UserModel> GetUsers()
    {
        return Users;
    }
}