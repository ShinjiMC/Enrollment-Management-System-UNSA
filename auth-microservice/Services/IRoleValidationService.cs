namespace auth_microservice.Services
{
    public interface IRoleValidationService
    {
        bool ValidateRole(string role);
    }
}
