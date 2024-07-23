using MongoDB.Driver;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Repositories.Data;

namespace PaymentsMicroservice.Repositories.Implementations
{
    public class ElectronicBillRepository : IElectronicBillRepository
    {
        private readonly MongoDbContext _context;

        public ElectronicBillRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<ElectronicBill> GetElectronicBillById(string electronicBillId)
        {
            return await _context.ElectronicBills.Find(e => e.ElectronicBillId == electronicBillId).FirstOrDefaultAsync();
        }

        public async Task SaveElectronicBill(ElectronicBill electronicBill)
        {
            var existingBill = await _context.ElectronicBills.Find(e => e.ElectronicBillId == electronicBill.ElectronicBillId).FirstOrDefaultAsync();
            if (existingBill == null)
            {
                await _context.ElectronicBills.InsertOneAsync(electronicBill);
            }
            else
            {
                await _context.ElectronicBills.ReplaceOneAsync(e => e.ElectronicBillId == electronicBill.ElectronicBillId, electronicBill);
            }
        }

        public Task<List<ElectronicBill>> GetElectronicBills()
        {
            return _context.ElectronicBills.Find(e => true).ToListAsync();
        }
    }
}
