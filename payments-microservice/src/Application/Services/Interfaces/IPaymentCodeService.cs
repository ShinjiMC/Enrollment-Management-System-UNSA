using PaymentsMicroservice.Application.Dtos;

namespace PaymentsMicroservice.Application.Services.Interfaces
{
    public interface IPaymentCodeService
    {
        PaymentCodeDto GeneratePaymentCode(string studentId, string electronicBillId);
        bool SavePaymentCode(PaymentCodeDto paymentCodeDto);
        PaymentCodeDto GetPaymentCodeById(string paymentCodeId);
        void MarkPaymentCodeAsUsed(string paymentCodeId);
        List<PaymentCodeDto> GetPaymentCodes();
    }
}
