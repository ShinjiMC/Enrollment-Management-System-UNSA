using NotificationsMicroservice.Domain.Entities;
using NotificationsMicroservice.Domain.Repositories;
using NotificationsMicroservice.Repositories.Data;
using MongoDB.Driver;
using MongoDB.Bson;

namespace NotificationsMicroservice.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MongoDbContext _context;

        public NotificationRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notifications.Find(_ => true).ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications.Find(notification => notification.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            await _context.Notifications.InsertOneAsync(notification);
            return notification;
        }

        public async Task UpdateAsync(Notification notification)
        {
            await _context.Notifications.ReplaceOneAsync(existingNotification => existingNotification.Id == notification.Id, notification);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Notifications.DeleteOneAsync(notification => notification.Id == id);
        }
        public async Task<int> GetNextIdAsync()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "notification_id");
            var update = Builders<BsonDocument>.Update.Inc("seq", 1);
            var options = new FindOneAndUpdateOptions<BsonDocument>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };
    
            var result = await _context.Counters.FindOneAndUpdateAsync(filter, update, options);
            return result["seq"].AsInt32;
        }
    }
}
