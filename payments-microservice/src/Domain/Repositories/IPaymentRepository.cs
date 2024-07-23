using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentById(string paymentId);
        Task<Payment> SavePayment(Payment payment);
    }
}
