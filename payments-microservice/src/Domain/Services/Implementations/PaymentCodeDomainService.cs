using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Services.Implementations
{
    public class PaymentCodeService
    {
        public PaymentCode GeneratePaymentCode(string studentId)
        {
            // Lógica para generar un nuevo código de pago
            return new PaymentCode(
                "newId", // Generar un ID único
                "uniqueCode", // Generar un código único
                studentId,
                false
            );
        }

        public bool ValidatePaymentCode(string code)
        {
            // Lógica para validar el código de pago
            return true; // Implementar validación real
        }

        public void MarkPaymentCodeAsUsed(string code)
        {
            // Lógica para marcar el código de pago como utilizado
        }
    }
}
