using System.Threading.Tasks;
using GrpcService2.Domain.Entities;

namespace GrpcService2.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountById(string id);
        Task CreateAccount(Account account);
        Task UpdateAccount(Account account);
        Task DeleteAccount(string id);
    }
}

