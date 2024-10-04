using Grpc.Core;
using GrpcService2.Data;
using GrpcService2.Models;
using GrpcService2;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GrpcService2.Services
{
    public class UserService : GrpcService2.UserService.UserServiceBase
    {
        private readonly UserContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(UserContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email
            };

            _logger.LogInformation($"Name={user.Name}, Email={user.Email}, Id={user.Id},");
            try
            {
                await _context.Users.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Inserting user: {ex.Message}");
                throw;
            }
            return new CreateUserResponse
            {
                Id = user.Id.ToString()
            };

        }

        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = await _context.Users.Find(u => u.Id == request.Id).FirstOrDefaultAsync();

            if (user == null)
            {
                _logger.LogWarning($"User not found for ID: {request.Id}");
                throw new RpcException(new Status(StatusCode.NotFound, "User not found."));
            }
            return new GetUserResponse
            {
                User = new User
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }
    }
}
