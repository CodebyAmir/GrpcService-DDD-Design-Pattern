using Grpc.Net.Client;
using GrpcService2;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5295");
        var client = new AccountService.AccountServiceClient(channel);

        while (true)
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1: Create Account");
            Console.WriteLine("2: Get Account");
            Console.WriteLine("3: Update Balance");
            Console.WriteLine("4: Delete Account");
            Console.WriteLine("5: Exit");
            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    Console.WriteLine("Enter account holder's name:");
                    string accountHolderName = Console.ReadLine();

                    Console.WriteLine("Enter initial balance:");
                    double initialBalance;
                    while (!double.TryParse(Console.ReadLine(), out initialBalance))
                    {
                        Console.WriteLine("Please enter a valid number for the initial balance.");
                    }

                    var createResponse = await client.CreateAccountAsync(new CreateAccountRequest
                    {
                        AccountHolderName = accountHolderName,
                        InitialBalance = initialBalance
                    });

                    Console.WriteLine($"Created Account ID: {createResponse.Id}");
                    break;

                case "2":
                    Console.WriteLine("Enter Account ID to get:");
                    string accountIdToGet = Console.ReadLine();

                    var getResponse = await client.GetAccountAsync(new GetAccountRequest
                    {
                        Id = accountIdToGet
                    });

                    var account = getResponse.Account;
                    Console.WriteLine($"Account ID: {account.Id}, Holder Name: {account.AccountHolderName}, Balance: {account.Balance}");
                    break;

                case "3":
                    Console.WriteLine("Enter Account ID to update balance:");
                    string accountIdToUpdate = Console.ReadLine();

                    Console.WriteLine("Enter amount to add/subtract from balance (use negative number to subtract):");
                    double amount;
                    while (!double.TryParse(Console.ReadLine(), out amount))
                    {
                        Console.WriteLine("Please enter a valid number for the change in balance.");
                    }

                    var updateResponse = await client.UpdateBalanceAsync(new UpdateBalanceRequest
                    {
                        Id = accountIdToUpdate,
                        Amount = amount
                    });

                    Console.WriteLine($"Updated Account ID: {updateResponse.Id}, New Balance: {updateResponse.NewBalance}");
                    break;

                case "4":
                    Console.WriteLine("Enter Account ID to delete:");
                    string accountIdToDelete = Console.ReadLine();

                    var deleteResponse = await client.DeleteAccountAsync(new DeleteAccountRequest
                    {
                        Id = accountIdToDelete
                    });

                    Console.WriteLine(deleteResponse.Message);
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }
    }
}