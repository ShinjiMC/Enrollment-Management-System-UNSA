namespace PaymentsMicroservice.Domain.Services.Interfaces
{
    using PaymentsMicroservice.Domain.Entities;

    public interface IPaymentCodeDomainService
    {
        PaymentCode? GeneratePaymentCode(string studentId, string electronicBillId);
        bool ValidateId(string payerId);
        void MarkPaymentCodeAsUsed(string code);
    }
}
