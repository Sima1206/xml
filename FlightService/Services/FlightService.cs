using FlightService.Configuration;
using FlightService.Model;
using FlightService.Model.dto;
using FlightService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Services
{
    public class FlightService : IFlightService
    {
        private readonly ProjectConfiguration _configuration;

        public FlightService(ProjectConfiguration projectConfiguration)
        {
            _configuration = projectConfiguration;
        }

        public IEnumerable<Flight> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                return unitOfWork.Flights.GetAll();
            }
            catch (Exception e)
            {
                return new List<Flight>();
            }
        }

        public Flight Get(string Id)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                return unitOfWork.Flights.Get(Id);
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public bool Delete(string id)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                unitOfWork.Flights.Delete(id);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Flight Edit(FlightDTO flightDTO)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);
                Flight flight = unitOfWork.Flights.Get(flightDTO.Id);

                flight.AirportDestination = flightDTO.AirportDestination;
                flight.AirportDeparture = flightDTO.AirportDeparture;
                flight.DepartureTime = flightDTO.DepartureTime;
                flight.ArrivalTime = flightDTO.ArrivalTime;
                flight.Duration = flightDTO.Duration;
                flight.TicketPrice = flightDTO.TicketPrice;
                flight.Capacity = flightDTO.Capacity;

                unitOfWork.Flights.Update(flight);

                return flight;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public Flight Create(Airport airportdestination, Airport airportdeparture, DateTime departuretime, DateTime arrivaltime, string duration, double price, int capacity)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                Flight flight = new Flight();
                flight.AirportDestination = airportdestination;
                flight.AirportDeparture = airportdeparture;
                flight.DepartureTime = departuretime;
                flight.ArrivalTime = arrivaltime;
                flight.Duration = duration;
                flight.TicketPrice = price;
                flight.Capacity = capacity;


                unitOfWork.Flights.Add(flight);

                return flight;

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
