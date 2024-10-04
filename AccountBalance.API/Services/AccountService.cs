using Grpc.Core;
using AccountBalance.Application.Interfaces;
using AccountBalance.API;

namespace AccountBalance.API.Services
{
    public class AccountService : AccountService.AccountServiceBase
    {
        private readonly IAccountService _accountService;

        public AccountService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public override async Task<AccountResponse> CreateAccount(CreateAccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.CreateAccountAsync(request.Name);
            return new AccountResponse { Id = account.Id.ToString(), Name = account.Name, Balance = (double)account.Balance };
        }

        public override async Task<AccountResponse> GetAccount(GetAccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.GetAccountAsync(Guid.Parse(request.Id));
            return new AccountResponse { Id = account.Id.ToString(), Name = account.Name, Balance = (double)account.Balance };
        }

        public override async Task<BalanceResponse> Credit(CreditRequest request, ServerCallContext context)
        {
            var balance = await _accountService.CreditAsync(Guid.Parse(request.Id), (decimal)request.Amount);
            return new BalanceResponse { Balance = (double)balance };
        }

        public override async Task<BalanceResponse> Debit(DebitRequest request, ServerCallContext context)
        {
            var balance = await _accountService.DebitAsync(Guid.Parse(request.Id), (decimal)request.Amount);
            return new BalanceResponse { Balance = (double)balance };
        }
    }
}
