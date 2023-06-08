using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Services
{
    public interface IUserService
    {
        User GetUserWithEmail(string email);
        User Registration(string email, string password, string name, string surname, string sSN, string phoneNumber);
        User Login(string email, string password);

    }
}
