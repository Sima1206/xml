using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class User : Entity
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String SSN { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String PhoneNumber { get; set; }
        public UserType UserType { get; set; }
    }
}
