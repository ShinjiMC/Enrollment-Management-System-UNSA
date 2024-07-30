using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using enrollments_microservice.Domain.Entities;

namespace enrollments_microservice.Repositories.Data;
public class Counter
{
    [BsonId]
    public string? Id { get; set; }
    public int SequenceValue { get; set; }
}

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly ILogger<MongoDbContext> _logger;

    public MongoDbContext(IConfiguration configuration, ILogger<MongoDbContext> logger)
    {
        _logger = logger;

        var connectionString = configuration.GetSection("MongoDb:ConnectionString").Value;
        var databaseName = configuration.GetSection("MongoDb:DatabaseName").Value;

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
            throw new InvalidOperationException("MongoDB configuration is missing.");

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);

        try
        {
            _database.RunCommandAsync((Command<BsonDocument>)"{ ping: 1 }").Wait();
            _logger.LogInformation("MongoDB connection validation succeeded.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MongoDB connection validation failed.");
            throw new InvalidOperationException("MongoDB connection validation failed.", ex);
        }
    }

    public IMongoCollection<CourseModel> CourseModel =>
        _database.GetCollection<CourseModel>("CoursesEnrolls");

    public IMongoCollection<EnrollModel> EnrollModel =>
        _database.GetCollection<EnrollModel>("Enrollments");

    public IMongoCollection<Counter> Counters =>
        _database.GetCollection<Counter>("Counters");

    public int GetNextSequenceValue(string sequenceName)
    {
        var filter = Builders<Counter>.Filter.Eq(c => c.Id, sequenceName);
        var update = Builders<Counter>.Update.Inc(c => c.SequenceValue, 1);
        var options = new FindOneAndUpdateOptions<Counter> { IsUpsert = true, ReturnDocument = ReturnDocument.After };

        var counter = Counters.FindOneAndUpdate(filter, update, options);
        return counter.SequenceValue;
    }

    public async Task InitializeCounterAsync()
    {
        var filter = Builders<Counter>.Filter.Eq(c => c.Id, "enroll_model_id");
        var update = Builders<Counter>.Update.SetOnInsert(c => c.SequenceValue, 0);
        var options = new FindOneAndUpdateOptions<Counter> { IsUpsert = true };

        await _database.GetCollection<Counter>("Counters").FindOneAndUpdateAsync(filter, update, options);
    }
}