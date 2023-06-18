using Grpc.Core;
using Proto1;
using Proto2;
using ReservationService.Core;

namespace ReservationService.Services;

public class AccommodationGrpcService: AccommodationGrpc.AccommodationGrpcBase
{
    private readonly IAccommodationService _accommodationRepository;
    public AccommodationGrpcService(IAccommodationService accommodationService)
    {
        _accommodationRepository = accommodationService;
    }


    public override Task<AccommodationResponse> DeleteAccommodationsByHostId(AccommodationRequest request, ServerCallContext context)
    {
        var executed =_accommodationRepository.DeleteAccommodationsByHostId(request.Request.Id);
        if (executed == false)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Error while deleting accommodations with host id:  '{request.Request.Id}'"));
        }
        var response = new AccommodationResponse();
        return Task.FromResult(response);
    }

}