using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Repositories
{
    public interface IPaymentCodeRepository
    {
        Task<PaymentCode> GetPaymentCodeById(string paymentCodeId);
        Task<List<PaymentCode>> GetPaymentCodes();
        Task SavePaymentCode(PaymentCode paymentCode);
    }
}
