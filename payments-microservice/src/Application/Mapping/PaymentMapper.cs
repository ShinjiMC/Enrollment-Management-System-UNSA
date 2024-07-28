using PaymentsMicroservice.Application.Dtos;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.ValueObjects;

namespace PaymentsMicroservice.Application.Mapping
{
    public static class PaymentMapper
    {
        public static PaymentDto ToDto(Payment payment)
        {
            return new PaymentDto
            {
                PaymentId = payment.PaymentId,
                Amount = new MoneyDto
                {
                    Amount = payment.Amount.Amount,
                    Currency = payment.Amount.Currency
                },
                PaymentDate = payment.PaymentDate,
                PaymentMethod = new PaymentMethodDto
                {
                    MethodType = payment.PaymentMethod.MethodType,
                    Details = payment.PaymentMethod.Details
                },
                StudentId = payment.StudentId,
                ElectronicBillId = payment.ElectronicBillId,
                Status = new PaymentStatusDto
                {
                    Status = payment.Status.Status
                }
            };
        }

        public static Payment ToEntity(PaymentDto paymentDto)
        {
            return new Payment
            {
                PaymentId = paymentDto.PaymentId,
                Amount = new Money(paymentDto.Amount.Amount, paymentDto.Amount.Currency),
                PaymentDate = paymentDto.PaymentDate,
                PaymentMethod = new PaymentMethod(paymentDto.PaymentMethod.MethodType, paymentDto.PaymentMethod.Details),
                StudentId = paymentDto.StudentId,
                ElectronicBillId = paymentDto.ElectronicBillId,
                Status = new PaymentStatus(paymentDto.Status.Status)
            };
        }
    }
}
