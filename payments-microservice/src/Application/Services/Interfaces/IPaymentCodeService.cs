using PaymentsMicroservice.Application.Dtos;

namespace PaymentsMicroservice.Application.Services.Interfaces
{
    public interface IPaymentCodeService
    {
        PaymentCodeDto GeneratePaymentCode(string studentId);
        PaymentCodeDto GetPaymentCodeById(string paymentCodeId);
        void MarkPaymentCodeAsUsed(string paymentCodeId);
    }
}
