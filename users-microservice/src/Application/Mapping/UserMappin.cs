using users_microservice.Application.Dtos;

namespace users_microservice.Application.Mapping
{
    public static class UserMapping
    {
        public static UserDto ToUserDto(this StudentDto studentDto)
        {
            return new UserDto
            {
                Id = studentDto.UserData?.Id ?? string.Empty,
                UserName = studentDto.UserData?.UserName ?? string.Empty,
                Password = studentDto.UserData?.Password ?? string.Empty,
                ConfirmPassword = studentDto.UserData?.ConfirmPassword ?? string.Empty,
                Role = studentDto.UserData?.Role ?? string.Empty
            };
        }

        public static UserDto ToUserDto(this AdminDto adminDto)
        {
            return new UserDto
            {
                Id = adminDto.UserData?.Id ?? string.Empty,
                UserName = adminDto.UserData?.UserName ?? string.Empty,
                Password = adminDto.UserData?.Password ?? string.Empty,
                ConfirmPassword = adminDto.UserData?.ConfirmPassword ?? string.Empty,
                Role = adminDto.UserData?.Role ?? string.Empty
            };
        }
    }
}
