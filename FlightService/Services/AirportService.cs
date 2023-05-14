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
    public class AirportService : IAirportService
    {
        private readonly ProjectConfiguration _configuration;

        public AirportService(ProjectConfiguration projectConfiguration)
        {
            _configuration = projectConfiguration;

        }

        public IEnumerable<Airport> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                return unitOfWork.Airports.GetAll();
            }
            catch (Exception e)
            {
                return new List<Airport>();
            }
        }

        public Airport Get(string Id)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);

                return unitOfWork.Airports.Get(Id);
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
                
                unitOfWork.Airports.Delete(id);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Airport Edit(AirportDTO airportDTO)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(_configuration);
                Airport airport = unitOfWork.Airports.Get(airportDTO.Id);

                airport.CityID = airportDTO.CityID;
                airport.Name = airportDTO.Name;
               
                unitOfWork.Airports.Update(airport);

                return airport;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
