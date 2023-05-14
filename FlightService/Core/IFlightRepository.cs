using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Core
{
    public interface IFlightRepository
    {
        Flight Get(string id);
        IEnumerable<Flight> GetAll();
        void Delete(string id);
        void Update(Flight flight);
        void Add(Flight flight);

    }
}
