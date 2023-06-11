using FlightService.Configuration;
using FlightService.Model;
using FlightService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Services
{
    public class UserService : IUserService
    {
        private readonly ProjectConfiguration _configuration;

        public UserService(ProjectConfiguration configuration) { _configuration = configuration; }


        public User Registration(string email, string password, string name, string surname, string ssn, string phoneNumber)
        {
            try
            {
                if (email == null || password == null || name == null || surname == null || phoneNumber == null || ssn == null)
                {
                    return null;
                }

                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                User user = unitOfWork.Users.GetUserWithEmail(email);

                if (user is not null)
                {
                    return null;
                }

                user = new User();
                user.Email = email;
                user.Password = BCrypt.Net.BCrypt.HashPassword(password) ;
                user.Name = name;
                user.Surname = surname;
                user.PhoneNumber = phoneNumber;
                user.SSN = ssn;


                unitOfWork.Users.Add(user);

                return user;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User Login(string email, string password)
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());

            foreach (User user in unitOfWork.Users.GetAll())
            {
                if (user.Email == email && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user;
                }
                
            }
            return null;
        } 

        public User GetUserWithEmail(string email)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                return unitOfWork.Users.GetUserWithEmail(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
