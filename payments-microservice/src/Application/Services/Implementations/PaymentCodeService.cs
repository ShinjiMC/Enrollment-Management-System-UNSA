using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Domain.Repositories;

namespace PaymentsMicroservice.Application.Services.Implementations
{
    public class PaymentCodeService : IPaymentCodeService
    {
        private readonly IPaymentCodeRepository _paymentCodeRepository;
        private readonly PaymentCodeService _paymentCodeService;

        public PaymentCodeService(IPaymentCodeRepository paymentCodeRepository, PaymentCodeService paymentCodeService)
        {
            _paymentCodeRepository = paymentCodeRepository;
            _paymentCodeService = paymentCodeService;
        }

        public PaymentCodeDto GeneratePaymentCode(string studentId)
        {
            var paymentCode = _paymentCodeService.GeneratePaymentCode(studentId);
            var paymentCodeDto = new PaymentCodeDto
            {
                PaymentCodeId = paymentCode.PaymentCodeId,
                Code = paymentCode.Code,
                StudentId = paymentCode.StudentId,
                IsUsed = paymentCode.IsUsed
            };
            
            // _paymentCodeRepository.SavePaymentCode(paymentCode);
            return paymentCodeDto;
        }

        public PaymentCodeDto GetPaymentCodeById(string paymentCodeId)
        {
            var paymentCode = _paymentCodeRepository.GetPaymentCodeById(paymentCodeId);
            if (paymentCode == null) return null;

            return new PaymentCodeDto
            {
                PaymentCodeId = paymentCode.PaymentCodeId,
                Code = paymentCode.Code,
                StudentId = paymentCode.StudentId,
                IsUsed = paymentCode.IsUsed
            };
        }

        public void MarkPaymentCodeAsUsed(string paymentCodeId)
        {
            var paymentCode = _paymentCodeRepository.GetPaymentCodeById(paymentCodeId);
            if (paymentCode != null)
            {
                paymentCode.MarkAsUsed();
                _paymentCodeRepository.SavePaymentCode(paymentCode);
            }
        }
    }
}
