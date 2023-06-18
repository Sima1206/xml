using Proto1;

namespace UserService.Core;

public interface IAccommodationService
{
    public Task<AccommodationResponse> DeleteAccommodationsByHostId(long id);
}