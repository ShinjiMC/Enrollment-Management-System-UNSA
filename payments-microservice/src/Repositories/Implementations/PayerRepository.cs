using MongoDB.Driver;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Repositories.Data;


namespace PaymentsMicroservice.Repositories.Implementations
{
    public class PayerRepository : IPayerRepository
    {
        private readonly MongoDbContext _context;

        public PayerRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payer>> GetPayers()
        {
            return await _context.Payers.Find(p => true).ToListAsync();
        }

        public async Task<Payer> GetPayerById(string payerId)
        {
            return await _context.Payers.Find(p => p.PayerId == payerId).FirstOrDefaultAsync();
        }

        public async Task<bool> SavePayer(Payer payer)
        {
            try
            {
                await _context.Payers.InsertOneAsync(payer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}