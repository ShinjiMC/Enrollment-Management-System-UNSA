using NotificationsMicroservice.Domain.Entities;
using NotificationsMicroservice.Domain.Repositories;
using NotificationsMicroservice.Repositories.Data;
using MongoDB.Driver;

namespace NotificationsMicroservice.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _context.Users.ReplaceOneAsync(existingUser => existingUser.Id == user.Id, user);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Users.DeleteOneAsync(user => user.Id == id);
        }
    }
}
