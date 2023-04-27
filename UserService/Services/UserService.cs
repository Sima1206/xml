using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                user.Email = dto.Email;
                user.CityId = dto.CityId;
                user.Enabled = true;
                user.userType = UserType.User;

                unitOfWork.Users.Add(user);
                unitOfWork.Complete();

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User UpdateProfile(UserDTO dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());

                User user = new User();

                user.Name = dto.Name;
                user.Surname = dto.Surname;
                user.Email = dto.Email;
                user.Password = dto.Password;
                user.CityId = dto.CityId;
                user.Enabled = true;
                user.userType = dto.userType;


                unitOfWork.Users.Update(user);
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
