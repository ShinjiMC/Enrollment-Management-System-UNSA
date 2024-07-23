using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PaymentsMicroservice.Repositories.Implementations
{
    public class PaymentCodeRepository : IPaymentCodeRepository
    {
        private readonly List<PaymentCode> _paymentCodes = new List<PaymentCode>();

        public async Task<PaymentCode> GetPaymentCodeById(string paymentCodeId)
        {
            return _paymentCodes.SingleOrDefault(p => p.PaymentCodeId == paymentCodeId);
        }

        public async Task SavePaymentCode(PaymentCode paymentCode)
        {
            var existingCode = _paymentCodes.SingleOrDefault(p => p.PaymentCodeId == paymentCode.PaymentCodeId);
            if (existingCode != null)
            {
                _paymentCodes.Remove(existingCode);
            }
            _paymentCodes.Add(paymentCode);
        }
    }
}
