using Grpc.Net.Client;
using Proto1;
using UserService.Core;

namespace UserService.Services;

public class AccommodationService : IAccommodationService
{
    public async Task<AccommodationResponse> DeleteAccommodationsByHostId(long id)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7140");
        var client = new AccommodationGrpc.AccommodationGrpcClient(channel);
        var request = new AccommodationRequest()  { Request = new AccommodationRequestDto{Id = id}};
        try
        {
            var reply = await client.DeleteAccommodationsByHostIdAsync(request);
            return reply;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"could not call server{ex.Message}");
            return null;
        }

    }
}
