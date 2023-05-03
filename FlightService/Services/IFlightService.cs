using FlightService.Model;
using FlightService.Model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Services
{
    public interface IFlightService
    {
        IEnumerable<Flight> GetAll();
        Flight Get(string Id);
        Boolean Delete(string id);
        Flight Edit(FlightDTO flightDTO);
        Flight Create(Airport airportdestination, Airport airportdeparture, DateTime departuretime, DateTime arrivaltime, string duration, double price, int capacity);

    }
}
