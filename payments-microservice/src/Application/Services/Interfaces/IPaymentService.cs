using PaymentsMicroservice.Application.Dtos;

namespace PaymentsMicroservice.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> CreatePayment(PaymentDto paymentDto);
        Task<bool> UpdatePaymentStatus(string paymentId, string status);
        Task<PaymentDto> GetPaymentById(string paymentId);
    }
}
