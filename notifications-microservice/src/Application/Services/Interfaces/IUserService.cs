
using NotificationsMicroservice.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(int id);
    }
}
