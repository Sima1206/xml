using Grpc.Net.Client;
using ReservationService.Core;
using Proto2;

namespace ReservationService.Services
{
    public class UserService : IUserService
    {
        public async Task<UserResponse> GetUserById(long id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7240");
            var client = new UserGrpc.UserGrpcClient(channel);
            var request = new UserRequest()  { Request = new UserRequestDto{Id = id}};
            try
            {
                var reply = await client.GetUserInfoAsync(request);
                return reply;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"could not call server{ex.Message}");
                return null;
            }

        }
    }
}