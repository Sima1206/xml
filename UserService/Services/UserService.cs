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

                var user = new User();

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

        public bool DeleteGuestAccount(long guestId)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var user = unitOfWork.Users.Get(guestId);
            if (user == null || user.Deleted || user.userType != (Model.UserType)UserType.Guest)
            {
                return false;
            }
            if (GuestHasReservations(guestId).Result)
            {
                return false;
            }
            user.Deleted = true;
            UpdateProfile(user);
            unitOfWork.Complete();
            return true;
        }
        
        public bool DeleteHostAccount(long hostId)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var user = unitOfWork.Users.Get(hostId);
            if (user == null || user.Deleted || user.userType != (Model.UserType)UserType.Host)
            {
                return false;
            }
            if (HostHasActiveReservations(hostId).Result)
            {
                return false;
            }
            new AccommodationService().DeleteAccommodationsByHostId(hostId);
            user.Deleted = true;
            UpdateProfile(user);
            unitOfWork.Complete();
            return true;
        }

        private async Task<bool> GuestHasReservations(long guestId)
        {
            return await new ReservationService().GuestHasReservations(guestId);
        }
        private async Task<bool> HostHasActiveReservations(long hostId)
        { 
            return await new ReservationService().HostHasActiveReservations(hostId);
        }

        public User GetUserByID(long id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());
                User user = unitOfWork.Users.GetUserByID(id);

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User Updatee(UserProfileDTO dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());
                User user = UserProfileDTO.ConvertToUser(dto);
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
