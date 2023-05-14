using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Core
{
    public interface IUserRepository
    {
        User GetUserWithEmail(string email);
        void Add(User user);
        User Get(string email);
    }
}
