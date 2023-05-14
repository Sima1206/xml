using UserService.Model;
using UserService.Model.DTO;

namespace UserService.Core
{
    public interface IUserService
    {
        User GetUserWithEmail(string email);
        User Register(RegistrationDTO dto);
        User UpdateProfile(User dto);
        bool DeleteGuestAccount(long guestId);
        bool DeleteHostAccount(long hostId);
    }
}
