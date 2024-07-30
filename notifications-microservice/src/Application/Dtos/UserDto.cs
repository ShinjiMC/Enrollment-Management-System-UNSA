using System.ComponentModel.DataAnnotations;

namespace NotificationsMicroservice.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        //[StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preference is required.")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Preference must be between 1 and 50 characters.")]
        public string Preference { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact Info is required.")]
        //[StringLength(200, MinimumLength = 1, ErrorMessage = "Contact Info must be between 1 and 200 characters.")]
        public string ContactInfo { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
