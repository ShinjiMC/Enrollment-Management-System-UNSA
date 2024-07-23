namespace PaymentsMicroservice.Domain.Services.Interfaces
{
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.ValueObjects;

    public interface IPaymentDomainService
    {
        Task<Payment> CreatePayment(Money amount, DateTime paymentDate, PaymentMethod paymentMethod, string studentId, string electronicBillId);
        Task<bool> UpdatePaymentStatus(string paymentId, PaymentStatus status);
        Task<Payment> GetPaymentById(string paymentId);
    }
}