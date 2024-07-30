using users_microservice.Application.Dtos;


namespace users_microservice.Repository.ExternalService
{
    public interface IExternalServiceAuth
    {
        // Métodos para manejar administradores
        Task<bool> RegisterUserAsync(UserDto userDto);
        // Métodos para manejar administradores
       
        
    }
}
