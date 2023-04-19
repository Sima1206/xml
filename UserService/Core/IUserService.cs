using UserService.Model;
using UserService.Model.DTO;

namespace UserService.Core
{
    public interface IUserService
    {
        User GetUserWithEmail(string email);
        User Register(RegistrationDTO dto);
    }
}
