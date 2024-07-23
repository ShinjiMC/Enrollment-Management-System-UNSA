using MongoDB.Driver;
using NotificationsMicroservice.Domain.Entities;

namespace NotificationsMicroservice.Repositories.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;


    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDbSettings:ConnectionString"];
        var databaseName = configuration["MongoDbSettings:DatabaseName"];
    
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString), "MongoDB connection string is not configured.");
    
        if (string.IsNullOrEmpty(databaseName))
            throw new ArgumentNullException(nameof(databaseName), "MongoDB database name is not configured.");
    
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }


        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("Users");

        public IMongoCollection<Notification> Notifications =>
            _database.GetCollection<Notification>("Notifications");
    }
}

