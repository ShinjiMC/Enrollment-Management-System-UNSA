namespace PaymentsMicroservice.Domain.Services.Interfaces
{
    using PaymentsMicroservice.Domain.Entities;

    public interface IPaymentCodeDomainService
    {
        PaymentCode GeneratePaymentCode(string studentId, string electronicBillId);
        bool ValidatePaymentCode(string code);
        void MarkPaymentCodeAsUsed(string code);
    }
}
