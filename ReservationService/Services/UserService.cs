using Grpc.Net.Client;
using ReservationService.Core;
using Proto2;

namespace ReservationService.Services
{
    public class UserService : IUserService
    {
        public async Task<UserResponse> GetUserById(long id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:4112");
            var client = new UserGrpc.UserGrpcClient(channel);
            var reply = await client.GetUserInfoAsync(new UserRequest() { Id = id });
            return reply;
        }
    }
}