using MongoDB.Driver;
using PaymentsMicroservice.Domain.Entities;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Repositories.Data;


namespace PaymentsMicroservice.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MongoDbContext _context;

        public PaymentRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentById(string paymentId)
        {
            // print por consola:
            Console.WriteLine("GetPaymentById: " + paymentId);
            return await _context.Payments.Find(p => p.PaymentId == paymentId).FirstOrDefaultAsync();
        }

        public async Task SavePayment(Payment payment)
        {
            var existingPayment = await GetPaymentById(payment.PaymentId);
            if (existingPayment == null)// insert one async if not exists
            {
                await _context.Payments.InsertOneAsync(payment);
            }
            else // update one async if exists
            {
                var filter = Builders<Payment>.Filter.Eq(p => p.PaymentId, payment.PaymentId);
                var update = Builders<Payment>.Update
                    .Set(p => p.Amount, payment.Amount)
                    .Set(p => p.PaymentDate, payment.PaymentDate)
                    .Set(p => p.PaymentMethod, payment.PaymentMethod)
                    .Set(p => p.StudentId, payment.StudentId)
                    .Set(p => p.ElectronicBillId, payment.ElectronicBillId)
                    .Set(p => p.Status, payment.Status);

                await _context.Payments.UpdateOneAsync(filter, update);
            }
        }
    }
}
