using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Repositories
{
    public interface IPaymentCodeRepository
    {
        PaymentCode GetPaymentCodeById(string paymentCodeId);
        void SavePaymentCode(PaymentCode paymentCode);
    }
}
