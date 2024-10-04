using AccountBalance.Application.Interfaces;
using AccountBalance.Application.Services;
using AccountBalance.Infrastructure.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add MongoDB connection
builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddSingleton(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    return client.GetDatabase("AccountBalanceDb");
});

// Register the account service and repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();

// Add gRPC services
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<AccountService>();
app.Run();

