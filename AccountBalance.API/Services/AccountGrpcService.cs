using Grpc.Core;
using AccountBalance.Application.Services;
using AccountBalance.Domain.Entities;

namespace AccountBalance.API.Services
{
    public class AccountGrpcService : AccountService.AccountServiceBase
    {
        private readonly AccountService _accountService;

        public AccountGrpcService(AccountService accountService)
        {
            _accountService = accountService;
        }

        public override async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.CreateAccountAsync(request.Name, request.Balance);
            return new CreateAccountResponse { Id = account.Id };
        }

        public override async Task<GetAccountResponse> GetAccount(GetAccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.GetAccountByIdAsync(request.Id);
            return new GetAccountResponse
            {
                Id = account.Id,
                Name = account.Name,
                Balance = account.Balance
            };
        }
    }
}
