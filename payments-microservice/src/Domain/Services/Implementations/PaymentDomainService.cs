using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Domain.Services.Implementations
{

    public class PaymentDomainService : IPaymentDomainService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentDomainService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> CreatePayment(Money amount, DateTime paymentDate, PaymentMethod paymentMethod, string studentId, string electronicBillId)
        {
            var payment = new Payment
            {
                Amount = amount,
                PaymentDate = paymentDate,
                PaymentMethod = paymentMethod,
                StudentId = studentId,
                ElectronicBillId = electronicBillId,
                Status = new PaymentStatus("Pending")
            };
            var savedPayment = await _paymentRepository.SavePayment(payment);
            return savedPayment;
        }

        public async Task<bool> UpdatePaymentStatus(string paymentId, PaymentStatus status)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);
            if (payment == null)
            {
                return false;
            }

            payment.Status = status;
            await _paymentRepository.SavePayment(payment);
            return true;
        }

        public async Task<Payment> GetPaymentById(string paymentId)
        {
            return await _paymentRepository.GetPaymentById(paymentId);
        }
    }
}