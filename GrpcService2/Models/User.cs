using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrpcService2.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
        
        public List<Guid> AccountIds { get; set; } = new List<Guid>();
    }
}
