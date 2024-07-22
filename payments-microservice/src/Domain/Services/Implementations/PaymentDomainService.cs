using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Services.Implementations
{
    public class PaymentProcessingService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentProcessingService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public PaymentStatus ProcessPayment(Payment payment)
        {
            // Process payment
            return new PaymentStatus("processing");
        }

        public bool VerifyPayment(Payment payment)
        {
            // Verify payment
            return true;
        }

    }
}
