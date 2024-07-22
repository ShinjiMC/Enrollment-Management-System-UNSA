namespace PaymentsMicroservice.Domain.Repositories
{
    using PaymentsMicroservice.Domain.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment> GetByIdAsync(Guid id);
    }
}
