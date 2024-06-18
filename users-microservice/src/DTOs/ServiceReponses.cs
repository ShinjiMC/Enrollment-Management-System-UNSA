namespace users_microservice.DTOs;

public class ServiceResponses
{
    public record class GeneralResponse(bool Flag, string Message, int StatusCode);
    public record class LoginResponse(bool Flag, string Token, string Message, int StatusCode);
}
