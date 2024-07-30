using users_microservice.Domain.Entities;
using users_microservice.Application.Dtos;

namespace users_microservice.Application.Mapping
{
    public static class AdminMapping
    {
        public static AdminDto ToDto(this AdminModel admin)
        {
            return new AdminDto
            {
               
                FullName = admin.FullName ?? string.Empty,
                Email = admin.Email ?? string.Empty,
                PhoneNumber = admin.PhoneNumber
            };
        }
        public static AdminModel ToModel(AdminDto adminDto)
        {
            return new AdminModel
            {
                FullName = adminDto.FullName,
                Email = adminDto.Email,
                PhoneNumber = adminDto.PhoneNumber
            };
        }
    }
}
