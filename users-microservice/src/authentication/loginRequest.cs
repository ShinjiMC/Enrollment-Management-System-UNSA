namespace users_microservice.authentication;

public sealed record LoginRequest(int userId, string username);