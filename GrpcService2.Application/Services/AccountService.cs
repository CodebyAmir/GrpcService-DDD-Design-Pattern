using GrpcService2.Domain.Entities;
using GrpcService2.Domain.Interfaces;
using System.Threading.Tasks;

namespace GrpcService2.Application.Services
{
    public class AccountManagementService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountManagementService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> GetAccountById(string id)
        {
            return await _accountRepository.GetAccountById(id);
        }

        public async Task<Account> CreateAccount(string accountHolderName, decimal initialBalance)
        {
            var account = new Account
            {
                AccountHolderName = accountHolderName,
                Balance = initialBalance
            };
            await _accountRepository.CreateAccount(account);
            return account;
        }

        public async Task UpdateAccount(Account account)
        {
            await _accountRepository.UpdateAccount(account);
        }

        public async Task UpdateAccountBalance(string id, decimal amount)
        {
            var account = await _accountRepository.GetAccountById(id);
            if (account == null) 
            {
                throw new KeyNotFoundException("Account not found");
            }
            account.UpdateBalance(amount);
            await _accountRepository.UpdateAccount(account);
        }

        public async Task DeleteAccount(string id)
        {
            await _accountRepository.DeleteAccount(id);
        }
    }
}
