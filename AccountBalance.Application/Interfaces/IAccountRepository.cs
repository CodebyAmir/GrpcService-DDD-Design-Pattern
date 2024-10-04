using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountBalance.Domain.Entities;
using System.Collections.Generic;

namespace AccountBalance.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(string id);
        Task<List<Account>> GetAllAsync();
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(string id);
    }
}
