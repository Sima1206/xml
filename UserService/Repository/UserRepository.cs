using Microsoft.EntityFrameworkCore;
using UserService.Core;
using UserService.Model;

namespace UserService.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository 
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User GetUserWithEmail(string email)
        {
            return ApplicationContext.Users.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
