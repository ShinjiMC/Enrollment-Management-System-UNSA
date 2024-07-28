using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Services.Interfaces;

namespace PaymentsMicroservice.Domain.Services.Implementations
{
    public class PaymentCodeDomainService : IPaymentCodeDomainService
    {
        public PaymentCode GeneratePaymentCode(string studentId, string electronicBillId)
        {
            // Lógica para generar un nuevo código de pago
            var newPaymentCode = new PaymentCode
            {
                // random guui
                Code = Guid.NewGuid().ToString(),
                StudentId = studentId,
                ElectronicBillId = electronicBillId,
                IsUsed = true
            };
            // print code
            Console.WriteLine($"New payment code generated: {newPaymentCode.Code}");
            return newPaymentCode;
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
