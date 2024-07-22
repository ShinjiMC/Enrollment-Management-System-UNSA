using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Application.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentProcessingService _paymentProcessingService;

        public PaymentService(IPaymentRepository paymentRepository, IPaymentProcessingService paymentProcessingService)
        {
            _paymentRepository = paymentRepository;
            _paymentProcessingService = paymentProcessingService;
        }

        public PaymentDto GetPaymentById(string paymentId)
        {
            var payment = _paymentRepository.GetPaymentById(paymentId);
            return new PaymentDto
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount.Amount,
                Currency = payment.Amount.Currency,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod.MethodType,
                StudentId = payment.StudentId,
                ElectronicBillId = payment.ElectronicBillId,
                Status = payment.Status.Status
            };
        }

        public void ProcessPayment(PaymentDto paymentDto)
        {
            var payment = new Payment(
                paymentDto.PaymentId,
                new Money(paymentDto.Amount, paymentDto.Currency),
                paymentDto.PaymentDate,
                new PaymentMethod(paymentDto.PaymentMethod, string.Empty),
                paymentDto.StudentId,
                paymentDto.ElectronicBillId,
                new PaymentStatus(paymentDto.Status)
            );

            _paymentProcessingService.ProcessPayment(payment);
            if (_paymentProcessingService.VerifyPayment(payment))
            {
                payment.UpdateStatus(new PaymentStatus("completed"));
                _paymentRepository.SavePayment(payment);
            }
        }
    }
}
