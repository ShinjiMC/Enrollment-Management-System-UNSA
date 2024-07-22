namespace PaymentsMicroservice.Domain.Repositories
{
    using PaymentsMicroservice.Domain.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IPaymentCodeRepository
    {
        Task AddAsync(PaymentCode paymentCode);
        Task<PaymentCode> GetByPaymentCodeAsync(string code);
    }
}
