using UserService.Core;
using UserService.Model;
using UserService.Model.DTO;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        public User GetUserWithEmail(string email)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());
                return unitOfWork.Users.GetUserWithEmail(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User Register(RegistrationDTO dto) 
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                User user = new User();

                user.Name = dto.Name;
                user.Surname = dto.Surname;
                user.Password = dto.Password;
                user.Email = dto.Email;
                user.CityId = dto.CityId;
                user.Enabled = true;

                unitOfWork.Users.Add(user);
                unitOfWork.Complete();

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
