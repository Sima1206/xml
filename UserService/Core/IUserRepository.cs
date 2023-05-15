using UserService.Model;

namespace UserService.Core
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserWithEmail(string email);
        User GetUserByID(long id);
    }
}
