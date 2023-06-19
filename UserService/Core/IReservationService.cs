namespace UserService.Core;
using Proto3;

public interface IReservationService
{
    public  Task<bool> GuestHasReservations(long id);

    public Task<bool> HostHasActiveReservations(long id);
}