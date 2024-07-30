using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Application.Mapping;

namespace PaymentsMicroservice.Application.Services.Implementations
{
    public class PaymentCodeService : IPaymentCodeService
    {
        private readonly IPaymentCodeRepository _paymentCodeRepository;
        private readonly IPaymentCodeDomainService _paymentCodeDomainService;

        public PaymentCodeService(IPaymentCodeRepository paymentCodeRepository, IPaymentCodeDomainService paymentCodeDomainService)
        {
            _paymentCodeRepository = paymentCodeRepository;
            _paymentCodeDomainService = paymentCodeDomainService;
        }

        public PaymentCodeDto? GeneratePaymentCode(string studentId, string electronicBillId)
        {
            var paymentCode = _paymentCodeDomainService.GeneratePaymentCode(studentId, electronicBillId);
            if (paymentCode == null)
            {
                return null;
            }
            return PaymentCodeMapper.ToDto(paymentCode);
        }

        public bool SavePaymentCode(PaymentCodeDto paymentCodeDto)
        {
            try
            {
                var paymentCode = PaymentCodeMapper.ToEntity(paymentCodeDto);
                _paymentCodeRepository.SavePaymentCode(paymentCode);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public PaymentCodeDto GetPaymentCodeById(string paymentCodeId)
        {
            var paymentCode = _paymentCodeRepository.GetPaymentCodeById(paymentCodeId).Result;
            return PaymentCodeMapper.ToDto(paymentCode);
        }

        public void MarkPaymentCodeAsUsed(string paymentCodeId)
        {
            var paymentCode = _paymentCodeRepository.GetPaymentCodeById(paymentCodeId).Result;
            _paymentCodeDomainService.MarkPaymentCodeAsUsed(paymentCode.Code);
            _paymentCodeRepository.SavePaymentCode(paymentCode);
        }

        public List<PaymentCodeDto> GetPaymentCodes()
        {
            var paymentCodes = _paymentCodeRepository.GetPaymentCodes().Result;
            List<PaymentCodeDto> paymentCodesDto = new ();
            foreach (var paymentCode in paymentCodes)
            {
                paymentCodesDto.Add(PaymentCodeMapper.ToDto(paymentCode));
            }
            return paymentCodesDto;
        }
    }
}