namespace enrollments_microservice.Application.Dtos;
public class ServiceResponses
{
    public record class GeneralResponse(bool Flag, string Message, int StatusCode);
    public record class GeneralResponse<T>(bool Flag, string Message, int StatusCode, T Data);
}

public static class Messages
{
    public const string UserNotFound = "User not found";
    public const string SchoolNotFound = "School not found";
}