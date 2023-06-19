using Grpc.Core;
using Proto3;
using ReservationService.Core;

namespace ReservationService.Services;

public class ReservationGrpcService: ReservationGrpc.ReservationGrpcBase
{
    private readonly IReservationService _reservationRepository;
    public ReservationGrpcService(IReservationService reservationService)
    {
        _reservationRepository = reservationService;
    }


    public override Task<ReservationResponse> GuestHasReservations(ReservationRequest request, ServerCallContext context)
    {
        var executed =_reservationRepository.GuestHasReservations(request.Request.Id);
        var response = new ReservationResponse() 
            { Response = new ReservationResponseDto() { HasReservations = executed } };
        return Task.FromResult(response);
    }
    public override Task<ReservationResponse> HostHasActiveReservations(ReservationRequest request, ServerCallContext context)
    {
        var executed =_reservationRepository.HostHasActiveReservations(request.Request.Id);
        var response = new ReservationResponse() 
            { Response = new ReservationResponseDto() { HasReservations = executed } };
        return Task.FromResult(response);
    }
}