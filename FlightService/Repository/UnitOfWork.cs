using FlightService.Configuration;
using FlightService.Core;
using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProjectConfiguration projectConfiguration;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(ProjectConfiguration projectConfiguration)
        {
            this.projectConfiguration = projectConfiguration;
            Flights = new FlightRepository(projectConfiguration);
            Airports = new AirportRepository(projectConfiguration);
            Tickets = new TicketRepository(projectConfiguration);
            Users = new UserRepository(projectConfiguration);
        }

        public IFlightRepository Flights { get; private set; }
        public IAirportRepository Airports { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public IUserRepository Users { get; private set; }
        public object User { get; private set; }

        public void Dispose()
        {
        }
    }

}
