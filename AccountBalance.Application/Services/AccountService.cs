using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountBalance.Application.Interfaces;
using AccountBalance.Domain.Entities;

namespace AccountBalance.Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccountAsync(string name, double initialBalance)
        {
            var account = new Account(name, initialBalance);
            await _accountRepository.AddAsync(account);
            return account;
        }

        public async Task<Account> GetAccountByIdAsync(string id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            await _accountRepository.UpdateAsync(account);
        }

        public async Task DeleteAccountAsync(string id)
        {
            await _accountRepository.DeleteAsync(id);
        }
    }
}

