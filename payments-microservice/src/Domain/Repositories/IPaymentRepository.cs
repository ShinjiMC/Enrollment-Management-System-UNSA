using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Payment GetPaymentById(string paymentId);
        void SavePayment(Payment payment);
    }
}
