using MongoDB.Driver;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Repositories.Data;


namespace PaymentsMicroservice.Repositories.Implementations
{
    public class PaymentCodeRepository : IPaymentCodeRepository
    {
        private readonly MongoDbContext _context;

        public PaymentCodeRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentCode> GetPaymentCodeById(string paymentCodeId)
        {
            Console.WriteLine("GetPaymentCodeById: " + paymentCodeId);
            return await _context.PaymentCodes.Find(p => p.PaymentCodeId == paymentCodeId).FirstOrDefaultAsync();
        }

        public async Task<List<PaymentCode>> GetPaymentCodes()
        {
            return await _context.PaymentCodes.Find(p => true).ToListAsync();
        }

        public async Task SavePaymentCode(PaymentCode paymentCode)
        {
            var existingCode = await GetPaymentCodeById(paymentCode.PaymentCodeId);
            if (existingCode == null)
            {
                await _context.PaymentCodes.InsertOneAsync(paymentCode);
            }
            else
            {
                var filter = Builders<PaymentCode>.Filter.Eq(p => p.PaymentCodeId, paymentCode.PaymentCodeId);
                var update = Builders<PaymentCode>.Update
                    .Set(p => p.Code, paymentCode.Code)
                    .Set(p => p.StudentId, paymentCode.StudentId)
                    .Set(p => p.ElectronicBillId, paymentCode.ElectronicBillId)
                    .Set(p => p.IsUsed, paymentCode.IsUsed);

                await _context.PaymentCodes.UpdateOneAsync(filter, update);
            }
        }
    }
}
