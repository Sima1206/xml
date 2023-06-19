using Grpc.Net.Client;
using Proto3;
using UserService.Core;

namespace UserService.Services;

public class ReservationService :IReservationService
{
    public async Task<bool> GuestHasReservations(long id)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7140");
        var client = new ReservationGrpc.ReservationGrpcClient(channel);
        var request = new ReservationRequest()  { Request = new ReservationRequestDto{Id = id}};
        var reply = await client.GuestHasReservationsAsync(request); 
        return reply.Response.HasReservations;
            
    }
    public async Task<bool> HostHasActiveReservations(long id)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7140");
        var client = new ReservationGrpc.ReservationGrpcClient(channel);
        var request = new ReservationRequest()  { Request = new ReservationRequestDto{Id = id}};
        var reply = await client.HostHasActiveReservationsAsync(request);
        return reply.Response.HasReservations;
    }
}