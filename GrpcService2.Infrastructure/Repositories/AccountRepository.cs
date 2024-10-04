using GrpcService2.Domain.Entities;
using GrpcService2.Domain.Interfaces;
using GrpcService2.Infrastructure.Data;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace GrpcService2.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountRepository(MongoDbContext context)
        {
            _accounts = context.Accounts;
        }

        public async Task<Account> GetAccountById(string id)
        {
            Console.WriteLine($"Input ID for retrieval: {id}");
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ID format", nameof(id));
            }
            Console.WriteLine($"Attempting to retrieve account with ID: {id}");
            var account = await _accounts.Find(a => a.Id == objectId).FirstOrDefaultAsync();
            if (account == null)
            {
                Console.WriteLine("Account not found in the database.");
            }
            else
            {
                Console.WriteLine($"Account found: {account.AccountHolderName}, Balance: {account.Balance}");
            }

            return account;
        }

        public async Task CreateAccount(Account account)
        {
            account.Id = ObjectId.GenerateNewId();
            try
            {
                await _accounts.InsertOneAsync(account);
                Console.WriteLine($"Account created with ID: {account.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting account: {ex.Message}");
                throw; 
            }
        }

        public async Task UpdateAccount(Account account)
        {
            await _accounts.ReplaceOneAsync(a => a.Id == account.Id, account);
        }

        public async Task DeleteAccount(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ID format", nameof(id));
            }

            await _accounts.DeleteOneAsync(a => a.Id == objectId);
        }
    }
}