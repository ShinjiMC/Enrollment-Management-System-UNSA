using users_microservice.Application.Dtos;
using users_microservice.Application.Mapping;
using users_microservice.Application.Service.Interface;
using users_microservice.Domain.Services.Interfaces;

namespace users_microservice.Application.Service.Implementations
{
    public class ExternalServiceImpl : IExternalService
    {
        private readonly IStudentServiceDomain _studentServiceDomain;

        public ExternalServiceImpl(IStudentServiceDomain studentServiceDomain)
        {
            _studentServiceDomain = studentServiceDomain;
        }

        public async Task<ExtStudentCourseDto> GetStudentCourseData(string id)
        {
            var idStudent = int.Parse(id);

            var result = await _studentServiceDomain.GetCoursesByStudentId(idStudent);


            return StudentMapping.ToExtDto(result);
        }

        
    }
}