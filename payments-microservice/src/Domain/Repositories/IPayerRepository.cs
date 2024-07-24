using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Domain.Repositories
{
    public interface IPayerRepository
    {
        Task<List<Payer>> GetPayers();
        Task<Payer> GetPayerById(string payerId);
        Task SavePayer(Payer payer);
    }
}