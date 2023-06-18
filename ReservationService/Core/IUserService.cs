using Proto2;

namespace ReservationService.Core
{
    public interface IUserService
    {
        public Task<UserResponse> GetUserById(long id);
    }
}

