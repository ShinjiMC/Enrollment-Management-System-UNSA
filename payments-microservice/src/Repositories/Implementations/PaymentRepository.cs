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
            return await _context.Payments.Find(p => p.PaymentId == paymentId).FirstOrDefaultAsync();
        }

        public async Task<Payment> SavePayment(Payment payment)
        {
            var existingPayment = await GetPaymentById(payment.PaymentId);
            if (existingPayment == null)
            {
                await _context.Payments.InsertOneAsync(payment);
                // Después de insertar, recuperamos el objeto insertado desde la base de datos para asegurarnos de tener los valores actualizados
                return await GetPaymentById(payment.PaymentId);
            }
            else
            {
                var filter = Builders<Payment>.Filter.Eq(p => p.PaymentId, payment.PaymentId);
                var update = Builders<Payment>.Update
                    .Set(p => p.Amount, payment.Amount)
                    .Set(p => p.PaymentDate, payment.PaymentDate)
                    .Set(p => p.PaymentMethod, payment.PaymentMethod)
                    .Set(p => p.StudentId, payment.StudentId)
                    .Set(p => p.ElectronicBillId, payment.ElectronicBillId)
                    .Set(p => p.Status, payment.Status);

                // Actualizar y devolver el objeto actualizado
                var updatedPayment = await _context.Payments.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Payment>
                {
                    ReturnDocument = ReturnDocument.After
                });

                return updatedPayment;
            }
        }

    }
}
