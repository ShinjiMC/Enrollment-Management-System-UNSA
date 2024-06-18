
using users_microservice.DTOs;
using static users_microservice.DTOs.ServiceResponses;

namespace users_microservice.services;

public interface IAdminService 
{
    
    Task<GeneralResponse> CreateAdminAccount(UserDto userDto);
    Task<GeneralResponse> UpdateAdminAccount(UserDto userDto);
    Task<GeneralResponse> DeleteAdminAccount(string id);
}