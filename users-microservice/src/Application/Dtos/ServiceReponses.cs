namespace users_microservice.Application.Dtos
{
    public class ServiceResponses
    {
        public record class GeneralResponse(bool Flag, string Message, int StatusCode, string? id);

    }
}
