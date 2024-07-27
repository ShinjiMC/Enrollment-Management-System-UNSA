using NotificationsMicroservice.Application.Dtos;
using NotificationsMicroservice.Application.Services.Interfaces;
using NotificationsMicroservice.Domain.Entities;
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
            if (user == null)
                return null;

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
            // Validar que los datos del UserDto sean válidos
            if (string.IsNullOrWhiteSpace(userDto.Name) ||
                string.IsNullOrWhiteSpace(userDto.Preference) ||
                string.IsNullOrWhiteSpace(userDto.ContactInfo))
            {
                throw new ArgumentException("UserDto fields cannot be empty.");
            }

            var existingUser = await _userDomainService.GetUserByIdAsync(userDto.Id);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same ID already exists.");
            }

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
            // Validar que los datos del UserDto sean válidos
            if (string.IsNullOrWhiteSpace(userDto.Name) ||
                string.IsNullOrWhiteSpace(userDto.Preference) ||
                string.IsNullOrWhiteSpace(userDto.ContactInfo))
            {
                throw new ArgumentException("UserDto fields cannot be empty.");
            }

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
            var user = await _userDomainService.GetUserByIdAsync(id);
            if (user == null)
                throw new ArgumentException("User not found.");

            await _userDomainService.DeleteUserAsync(id);
        }
    }
}
