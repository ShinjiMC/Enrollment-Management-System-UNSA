using System.ComponentModel.DataAnnotations;
namespace NotificationsMicroservice.Application.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; } // ID será autoincrementado en la base de datos

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = string.Empty;// Type es requerido

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } = string.Empty;// Message es requerido
        
        public string Status { get; set; } = string.Empty; // Status no es requerido

        [Required(ErrorMessage = "RecipientId is required.")]
        public int RecipientId { get; set; } // RecipientId es requerido
    }
}
