using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Application.Mapping
{
    public static class PaymentCodeMapper
    {
        public static PaymentCodeDto ToDto(PaymentCode paymentCode)
        {
            return new PaymentCodeDto
            {
                PaymentCodeId = paymentCode.PaymentCodeId,
                Code = paymentCode.Code,
                StudentId = paymentCode.StudentId,
                ElectronicBillId = paymentCode.ElectronicBillId,
                IsUsed = paymentCode.IsUsed
            };
        }

        public static PaymentCode ToEntity(PaymentCodeDto paymentCodeDto)
        {
            return new PaymentCode
            {
                PaymentCodeId = paymentCodeDto.PaymentCodeId,
                Code = paymentCodeDto.Code,
                StudentId = paymentCodeDto.StudentId,
                ElectronicBillId = paymentCodeDto.ElectronicBillId,
                IsUsed = paymentCodeDto.IsUsed
            };
        }
    }
}
