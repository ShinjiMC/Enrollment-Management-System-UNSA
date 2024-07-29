using MongoDB.Driver;
using SchoolsMicroservice.Models;

namespace SchoolsMicroservice.Repositories.Data
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

        public IMongoCollection<StudyPlanCourse> StudyPlanCourse =>
            _database.GetCollection<StudyPlanCourse>("StudyPlanCourse");

        public IMongoCollection<Course> Courses =>
            _database.GetCollection<Course>("Courses");
        public IMongoCollection<CoursePrerequisite> CoursePrerequisites =>
            _database.GetCollection<CoursePrerequisite>("CoursePrerequisites");

        public IMongoCollection<Department> Departments =>
            _database.GetCollection<Department>("Departments");

        public IMongoCollection<School> Schools =>
            _database.GetCollection<School>("Schools");

        public IMongoCollection<Teacher> Teachers =>
            _database.GetCollection<Teacher>("Teachers");
    
    }
}