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
        User GetUserByID(long id);
        User Updatee(UserProfileDTO dto);
        User Login(string email, string password);
    }
}
