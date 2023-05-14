using FlightService.Model;
using FlightService.Model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Services
{
    public interface IAirportService
    {
        IEnumerable<Airport> GetAll();
        Airport Get(string Id);
        bool Delete(string id);
        Airport Edit(AirportDTO airportDTO);
    }
}
