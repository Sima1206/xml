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

        public bool DeleteGuestAccount(long guestId)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            var user = unitOfWork.Users.Get(guestId);
            if (user == null || user.Deleted || user.userType != UserType.Guest)
            {
                return false;
            }
            if (GuestHasReservations(guestId))
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
            if (user == null || user.Deleted || user.userType != UserType.Host)
            {
                return false;
            }
            if (HostHasActiveReservations(hostId))
            {
                return false;
            }
            //accomodationService.DeleteAccommodationsByHostId(hostId);
            user.Deleted = true;
            UpdateProfile(user);
            unitOfWork.Complete();
            return true;
            

            return true;
        }

        private bool GuestHasReservations(long guestId)
        {
            // var reservations = _reservationService.GetByGuestId(guestId);
            //return reservations.Any();
            return false;
        }
        private bool HostHasActiveReservations(long hostId)
        {
            // var reservations = _reservationService.GetByHostId(hostId).Where(reservation => reservation.Accepted == true);
            // var today = DateTime.Today;
            // return reservations.Any(r => r.StartDate <= today && r.EndDate >= today);
            return false;
        }

        public void IncreaseCancelCount(long id)
        {
            GetUserByID(id).cancelCount++;
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
