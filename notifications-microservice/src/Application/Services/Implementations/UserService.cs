
using NotificationsMicroservice.Application.Dtos;
using NotificationsMicroservice.Application.Services.Interfaces;
using NotificationsMicroservice.Domain.Entities; // Asegúrate de que esta línea esté incluida
using NotificationsMicroservice.Domain.Services.Interfaces;

namespace NotificationsMicroservice.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserDomainService _userDomainService;

        public UserService(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userDomainService.GetAllUsersAsync();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Preference = user.Preference,
                ContactInfo = user.ContactInfo,
                IsActive = user.IsActive
            }).ToList();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userDomainService.GetUserByIdAsync(id);
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Preference = user.Preference,
                ContactInfo = user.ContactInfo,
                IsActive = user.IsActive
            };
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Preference = userDto.Preference,
                ContactInfo = userDto.ContactInfo,
                IsActive = userDto.IsActive
            };
            var createdUser = await _userDomainService.CreateUserAsync(user);
            return new UserDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Preference = createdUser.Preference,
                ContactInfo = createdUser.ContactInfo,
                IsActive = createdUser.IsActive
            };
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Preference = userDto.Preference,
                ContactInfo = userDto.ContactInfo,
                IsActive = userDto.IsActive
            };
            await _userDomainService.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userDomainService.DeleteUserAsync(id);
        }
    }
}

