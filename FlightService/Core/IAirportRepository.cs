using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Core
{
    public interface IAirportRepository
    {
        Airport Get(string id);
        IEnumerable<Airport> GetAll();
        void Delete(string id);
        void Update(Airport airport);
    }
}
