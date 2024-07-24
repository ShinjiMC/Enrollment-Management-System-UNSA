namespace auth_microservice.Application.DTOs;


public record UserSession(string? Id, string? Name, string? Email, string? Role);
