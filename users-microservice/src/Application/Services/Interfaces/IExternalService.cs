using users_microservice.Application.Dtos;
using static users_microservice.Application.Dtos.ServiceResponses;

namespace users_microservice.Application.Service.Interface
{
    public interface IExternalService
    {
        
        // Métodos para manejar administradores
        Task<ExtStudentCourseDto> GetStudentCourseData(string id);
        
    }
}
