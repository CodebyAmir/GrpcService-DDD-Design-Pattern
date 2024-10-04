using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrpcService2.Domain.Entities
{
    public class Account
    {
        [BsonId]
        public ObjectId Id { get; set; }  

        public string AccountHolderName { get; set; }
        public decimal Balance { get; set; }

        public Account()
        {
            Id = ObjectId.GenerateNewId();
        }


        [BsonIgnore]
        public string IdString => Id.ToString();

        public void UpdateBalance(decimal amount)
        {
            if (Balance + amount < 0)
                throw new InvalidOperationException("Insufficient Balance for this operation.");
            Balance += amount;

        }
    }
}
