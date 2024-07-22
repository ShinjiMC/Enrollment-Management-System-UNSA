namespace PaymentsMicroservice.Domain.Services.Interfaces
{
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.ValueObjects;

    public interface IPaymentDomainService
    {
        PaymentStatus ProcessPayment(Payment payment);
        bool VerifyPayment(Payment payment);
    }
}