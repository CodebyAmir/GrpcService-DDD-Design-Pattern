using GrpcService2.Services;
using GrpcService2.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<UserContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserService>();

app.Run();

