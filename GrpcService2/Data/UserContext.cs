using GrpcService2.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace GrpcService2.Data
{
    public class UserContext
    {
        private readonly IMongoDatabase _db;

        public UserContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("Mongodb"));
            _db = client.GetDatabase("GrpcUserDb");
        }

        public IMongoCollection<User> Users => _db.GetCollection<User>("Users");
    }
}

