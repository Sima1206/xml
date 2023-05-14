using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model.dto
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientSecret { get; set; }
        public string ClientID { get; set; }
    }
}
