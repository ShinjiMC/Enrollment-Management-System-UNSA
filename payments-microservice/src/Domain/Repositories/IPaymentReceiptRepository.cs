namespace PaymentsMicroservice.Domain.Repositories
{
    using PaymentsMicroservice.Domain.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IPaymentReceiptRepository
    {
        Task AddAsync(PaymentReceipt paymentReceipt);
        Task<PaymentReceipt> GetByIdAsync(Guid id);
    }
}