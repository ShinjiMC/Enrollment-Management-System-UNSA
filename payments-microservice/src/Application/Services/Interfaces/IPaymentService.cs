using PaymentsMicroservice.Application.Dtos;

namespace PaymentsMicroservice.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        PaymentDto GetPaymentById(string paymentId);
        void ProcessPayment(PaymentDto paymentDto);
    }
}
