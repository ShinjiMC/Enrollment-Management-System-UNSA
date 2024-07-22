using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PaymentsMicroservice.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly List<Payment> _payments = new List<Payment>();

        public Payment GetPaymentById(string paymentId)
        {
            return _payments.SingleOrDefault(p => p.PaymentId == paymentId);
        }

        public void SavePayment(Payment payment)
        {
            var existingPayment = _payments.SingleOrDefault(p => p.PaymentId == payment.PaymentId);
            if (existingPayment != null)
            {
                _payments.Remove(existingPayment);
            }
            _payments.Add(payment);
        }
    }
}
