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
                user.userType = dto.userType;
                user.cancelCount = 0;
                unitOfWork.Users.Add(user);
                unitOfWork.Complete();

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User UpdateProfile(User dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());
                unitOfWork.Users.Update(dto);
                unitOfWork.Complete();

                return dto;
            }
            catch (Exception e)
            {
                return null;
            }
        }





    }
}
