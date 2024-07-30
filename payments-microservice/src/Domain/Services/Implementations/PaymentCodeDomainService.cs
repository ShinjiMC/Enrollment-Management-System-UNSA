using MongoDB.Bson;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Services.Interfaces;

namespace PaymentsMicroservice.Domain.Services.Implementations
{
    public class PaymentCodeDomainService : IPaymentCodeDomainService
    {
        public PaymentCode? GeneratePaymentCode(string studentId, string electronicBillId)
        {
            if (!ObjectId.TryParse(studentId, out _) || !ObjectId.TryParse(electronicBillId, out _))
            {
                return null;
            }
            // Lógica para generar un nuevo código de pago
            var newPaymentCode = new PaymentCode
            {
                // random guui
                Code = Guid.NewGuid().ToString(),
                StudentId = studentId,
                ElectronicBillId = electronicBillId,
                IsUsed = true
            };
            return newPaymentCode;
        }

        public bool ValidateId(string payerId)
        {
            return ObjectId.TryParse(payerId, out _); // verificar que id sea del tipo ObjectId de Mongodb
        }

        public void MarkPaymentCodeAsUsed(string code)
        {
            // Lógica para marcar el código de pago como utilizado
        }
    }
}
