using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Application.Services.Interfaces
{
    public interface IPayerService
    {
        Task<List<Payer>> GetPayers();
        Task<Payer> GetPayerById(string payerId);
        Task<bool> SavePayer(Payer payer);
    }
}