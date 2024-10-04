using Grpc.Core;
using GrpcService2.Application.Services;
using GrpcService2.Domain.Entities;
using System.Threading.Tasks;

namespace GrpcService2.API.Services
{
    public class AccountGrpcService : AccountService.AccountServiceBase
    {
        private readonly AccountManagementService _accountService;

        public AccountGrpcService(AccountManagementService accountService)
        {
            _accountService = accountService;
        }

        public override async Task<GetAccountResponse> GetAccount(GetAccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.GetAccountById(request.Id);
            if (account == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Account not found"));
            }

            return new GetAccountResponse
            {
                Account = new Account
                {
                    Id = account.IdString,
                    AccountHolderName = account.AccountHolderName,
                    Balance = (double)account.Balance
                }
            };
        }

        public override async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.AccountHolderName))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "AccountHolderName cannot be null or empty"));
            }

            if (request.InitialBalance <= 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "InitialBalance must be greater than zero"));
            }

            var account = new Domain.Entities.Account
            {
                AccountHolderName = request.AccountHolderName,
                Balance = (decimal)request.InitialBalance
            };

            try
            {
                var createdAccount = await _accountService.CreateAccount(account.AccountHolderName, account.Balance);
                Console.WriteLine($"Account created with ID: {createdAccount.IdString}");
                return new CreateAccountResponse
                {
                    Id = createdAccount.IdString
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Failed to create account"));
            }
        }

        public override async Task<UpdateBalanceResponse> UpdateBalance(UpdateBalanceRequest request, ServerCallContext context)
        {
            try
            {
                await _accountService.UpdateAccountBalance(request.Id, (decimal)request.Amount);
            }
            catch (InvalidOperationException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            var updatedAccount = await _accountService.GetAccountById(request.Id);
            
            return new UpdateBalanceResponse
            {
                Id = updatedAccount.IdString,
                NewBalance = (double)updatedAccount.Balance
            };
        }

        public override async Task<DeleteAccountResponse> DeleteAccount(DeleteAccountRequest request, ServerCallContext context)
        {
            var account = await _accountService.GetAccountById(request.Id);
            if (account == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Account not found"));
            }

            await _accountService.DeleteAccount(request.Id);

            return new DeleteAccountResponse
            {
                Id = request.Id,
                Message = "Account successfully deleted."
            };
        }

    }
}
