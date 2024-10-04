using GrpcService2.API.Services;
using GrpcService2.Application.Services;
using GrpcService2.Domain.Interfaces;
using GrpcService2.Infrastructure.Data;
using GrpcService2.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");

builder.Services.AddSingleton<MongoDbContext>(sp =>
    new MongoDbContext(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<AccountManagementService>();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<AccountGrpcService>();

app.Run();
