using MongoDB.Driver;
using PaymentsMicroservice.Domain.Entities;

namespace PaymentsMicroservice.Repositories.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDb:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDb:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<ElectronicBill> ElectronicBills =>
            _database.GetCollection<ElectronicBill>("ElectronicBills");

        public IMongoCollection<Payment> Payments =>
            _database.GetCollection<Payment>("Payments");

        public IMongoCollection<PaymentCode> PaymentCodes =>
            _database.GetCollection<PaymentCode>("PaymentCodes");

        public IMongoCollection<Payer> Payers =>
            _database.GetCollection<Payer>("Payers");
    }
}
