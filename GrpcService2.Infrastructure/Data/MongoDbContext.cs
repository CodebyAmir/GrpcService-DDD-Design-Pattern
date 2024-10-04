using MongoDB.Driver;

namespace GrpcService2.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("GrpcAccountDb");
        }

        public IMongoCollection<GrpcService2.Domain.Entities.Account> Accounts => _database.GetCollection<GrpcService2.Domain.Entities.Account>("Accounts");
    }
}
