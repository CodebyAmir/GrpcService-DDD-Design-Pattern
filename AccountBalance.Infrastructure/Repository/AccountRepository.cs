using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountBalance.Application.Interfaces;
using AccountBalance.Domain.Entities;
using MongoDB.Driver;

namespace AccountBalance.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountRepository(IMongoDatabase database)
        {
            _accounts = database.GetCollection<Account>("Accounts");
        }

        public async Task<Account> GetByIdAsync(string id)
        {
            return await _accounts.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _accounts.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Account account)
        {
            await _accounts.InsertOneAsync(account);
        }

        public async Task UpdateAsync(Account account)
        {
            await _accounts.ReplaceOneAsync(a => a.Id == account.Id, account);
        }

        public async Task DeleteAsync(string id)
        {
            await _accounts.DeleteOneAsync(a => a.Id == id);
        }
    }
}
