using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Mapping;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Application.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDomainService _paymentDomainService;

        public PaymentService(IPaymentDomainService paymentDomainService)
        {
            _paymentDomainService = paymentDomainService;
        }

        public async Task<PaymentDto> CreatePayment(PaymentDto paymentDto)
        {
            var payment = await _paymentDomainService.CreatePayment(
                new Money(paymentDto.Amount.Amount, paymentDto.Amount.Currency),
                paymentDto.PaymentDate,
                new PaymentMethod(paymentDto.PaymentMethod.MethodType, paymentDto.PaymentMethod.Details),
                paymentDto.StudentId,
                paymentDto.ElectronicBillId
            );
            return PaymentMapper.ToDto(payment);
        }

        public async Task<bool> UpdatePaymentStatus(string paymentId, string status)
        {
            var paymentStatus = new PaymentStatus(status);
            return await _paymentDomainService.UpdatePaymentStatus(paymentId, paymentStatus);
        }

        public async Task<PaymentDto> GetPaymentById(string paymentId)
        {
            var payment = await _paymentDomainService.GetPaymentById(paymentId);
            return PaymentMapper.ToDto(payment);
        }
    }

}